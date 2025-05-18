import { Component, effect, OnDestroy, OnInit } from '@angular/core';
import { PatientService } from '../../services/patient.service';
import { PatientInfoDto } from '../../services/apiClient';
import { NgxEchartsDirective, provideEcharts } from 'ngx-echarts';
import { CommonModule, DatePipe } from '@angular/common';
import { EChartsOption } from 'echarts';

@Component({
  selector: 'app-selected-patient-info',
  standalone: true,
  imports: [
    NgxEchartsDirective,
    DatePipe,
    CommonModule
  ],
  templateUrl: './selected-patient-info.component.html',
  styleUrl: './selected-patient-info.component.scss',
  providers: [provideEcharts()]
})
export class SelectedPatientInfoComponent implements OnDestroy{
  patientInfoDto: PatientInfoDto;
  heartRateChartOptions: EChartsOption;


  constructor(private patientService: PatientService){
    const patientInfoDto = this.patientService.getSelectedPatient();

    effect(() =>{
      this.patientInfoDto = patientInfoDto();
      console.log(this.patientInfoDto);
      if(!!this.patientInfoDto){
        this.setupHeartRateChart();
      }

    });
  }

  setupHeartRateChart(): void {
  const heartRates = this.patientInfoDto.heartRates ?? [];

  const sorted = heartRates
    .filter(hr => !!hr.timestamp)
    .sort((a, b) => new Date(a.timestamp).getTime() - new Date(b.timestamp).getTime());

  const times = sorted.map(hr => new Date(hr.timestamp).toLocaleTimeString());
  const values = sorted.map(hr => hr.measurement);

  const hasData = values.length > 0;

  this.heartRateChartOptions = {
    title: {
      text: 'Heart Rate Over Time'
    },
    tooltip: {
      trigger: 'axis'
    },
    xAxis: {
      type: 'category',
      data: times,
      name: 'Time',
      show: hasData
    },
    yAxis: {
      type: 'value',
      name: 'BPM',
      show: hasData
    },
    series: hasData
      ? [
          {
            name: 'Heart Rate',
            type: 'line',
            data: values,
            smooth: true,
            areaStyle: {},
            lineStyle: { width: 2 },
            itemStyle: {
              color: '#ff4d4f'
            }
          }
        ]
      : [],
    graphic: !hasData
      ? {
          type: 'text',
          left: 'center',
          top: 'middle',
          style: {
            text: 'No heart rate data available',
            fontSize: 18,
            fill: '#999'
          }
        }
      : undefined
  };
}

  ngOnDestroy(): void {
      this.patientService.selectedPatient.set(null);
  }
}


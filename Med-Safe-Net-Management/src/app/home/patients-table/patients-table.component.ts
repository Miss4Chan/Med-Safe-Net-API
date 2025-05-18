import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ApiClient, PatientDto } from '../../services/apiClient';
import { lastValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { PatientService } from '../../services/patient.service';

@Component({
  selector: 'app-patients-table',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './patients-table.component.html',
  styleUrl: './patients-table.component.scss'
})
export class PatientsTableComponent  implements OnInit{
  patients: PatientDto[] = [];

  constructor(private apiClient: ApiClient, private router: Router, private patientService: PatientService){

  }

  async onSelectPatient(patient: PatientDto) {
    const patientInfoDto = await lastValueFrom(this.apiClient.patientInfo(patient.id));
    this.patientService.selectedPatient.set(patientInfoDto);
    this.router.navigate(['/patient-info']);
  }

  async ngOnInit(): Promise<void> {
    this.patients = await lastValueFrom(this.apiClient.getPatients());
    // console.log(this.patients)
  }

}

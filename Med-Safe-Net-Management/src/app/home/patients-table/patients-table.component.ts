import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ApiClient, PatientDto } from '../../services/apiClient';
import { lastValueFrom } from 'rxjs';

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

  constructor(private apiClient: ApiClient){

  }

  onSelectPatient(patient: PatientDto) {
  }

  async ngOnInit(): Promise<void> {
    this.patients = await lastValueFrom(this.apiClient.getPatients());
    console.log(this.patients)
  }

}

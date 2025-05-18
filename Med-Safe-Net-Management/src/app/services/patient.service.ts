import { Injectable, signal } from '@angular/core';
import { PatientInfoDto } from './apiClient';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  selectedPatient = signal<PatientInfoDto>(null);

  getSelectedPatient(){
    return this.selectedPatient;
  }
}

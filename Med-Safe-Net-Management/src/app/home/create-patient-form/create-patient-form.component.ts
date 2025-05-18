import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { lastValueFrom } from 'rxjs';
import { ApiClient, RegisterDto } from '../../services/apiClient';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-patient-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './create-patient-form.component.html',
  styleUrl: './create-patient-form.component.scss',
})
export class CreatePatientFormComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private apiClient: ApiClient,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required]),
      dateOfBirth: [new Date(), Validators.required],
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }

  async onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }
    const registerDto = new RegisterDto({
      ...this.registerForm.value,
      dateOfBirth: new Date(this.registerForm.value.dateOfBirth),
    });
    const result = await lastValueFrom(
      this.apiClient.registerPatient(registerDto)
    );
    this.router.navigate(['/pratients']);
  }
}

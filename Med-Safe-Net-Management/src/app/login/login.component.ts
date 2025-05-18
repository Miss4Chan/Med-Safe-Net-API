import { NgClass } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  Validators,
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ApiClient, LoginDto } from '../services/apiClient';
import { lastValueFrom } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgClass, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private apiClient: ApiClient,
    private toasterService: ToastrService,
    private router: Router,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  async onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }

    const loginDto = new LoginDto(this.loginForm.value);

    try {
      const result = await lastValueFrom(this.apiClient.login(loginDto));
      this.accountService.currentUser.set(result);
      localStorage.setItem('user', JSON.stringify(result));

      this.toasterService.show('Login successful');
      await this.router.navigate(['/']);
    } catch (error) {
      console.error('Login failed', error);
      this.toasterService.error(
        'Login failed. Please check your credentials and try again.',
      );
    }
  }
}

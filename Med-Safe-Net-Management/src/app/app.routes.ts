import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: '',
    loadComponent: () =>
      import('./home/home.component').then((m) => m.HomeComponent),
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'patients', pathMatch: 'full' },
      {
        path: 'patients',
        loadComponent: () =>
          import('./home/patients-table/patients-table.component').then((m) => m.PatientsTableComponent),
      },
      {
        path: 'create-patient',
        loadComponent: () =>
          import('./home/create-patient-form/create-patient-form.component').then((m) => m.CreatePatientFormComponent),
      },
    ],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

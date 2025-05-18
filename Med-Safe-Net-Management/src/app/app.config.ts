import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter, withPreloading } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { routes } from './app.routes';
import { API_BASE_URL } from './services/apiClient';
import { environment } from '../environments/environment';
import { FlagBasedPreloadingStrategy } from './services/flag-based.preloading-strategy';
import { jwtInterceptor } from './services/jwt-interceptor.service';
import { provideToastr } from 'ngx-toastr';


export const appConfig: ApplicationConfig = {
    providers:[
      provideHttpClient(
      withInterceptors([jwtInterceptor]),
      withInterceptorsFromDi()
    ),
    provideToastr({
      positionClass: 'toast-top-left',
      closeButton: true, 
      timeOut: 5000, 
      progressBar: true  
    }),  
      { provide: API_BASE_URL, useValue: environment.apiUrl },
      provideAnimations(),
      provideRouter(routes,withPreloading(FlagBasedPreloadingStrategy))
    ]
}

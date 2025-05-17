import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter, withPreloading } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { JwtInterceptor, JwtModule } from '@auth0/angular-jwt';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch } from '@angular/common/http';
import { routes } from './app.routes';
import { API_BASE_URL } from './services/apiClient';
import { environment } from '../environments/environment';
import { FlagBasedPreloadingStrategy } from './services/flag-based.preloading-strategy';


export function tokenGetter() {
  // const account = JSON.parse(localStorage.getItem('user'));
  // return account ? account.token : 'noToken';
  return 'noToken';
}
export const appConfig: ApplicationConfig = {
    providers:[
      importProvidersFrom(
        JwtModule.forRoot({
          config: {
            tokenGetter: tokenGetter,
            disallowedRoutes: [],
          },
        }),
      ),
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
      { provide: API_BASE_URL, useValue: environment.apiUrl },
      provideHttpClient(withFetch()),
      provideAnimations(),
      provideRouter(routes,withPreloading(FlagBasedPreloadingStrategy))
    ]
}

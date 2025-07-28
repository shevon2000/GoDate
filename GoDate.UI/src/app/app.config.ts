import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { jwtInterceptor } from './interceptors/jwt.interceptor';
import { NgxSpinnerModule } from "ngx-spinner";
import { loadingInterceptor } from './interceptors/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideHttpClient(withInterceptors([jwtInterceptor, loadingInterceptor])),
    provideAnimations(),      
    provideToastr({                               // for toastr notifications
      positionClass: 'toast-bottom-right',
      timeOut: 3000,
      closeButton: true,
      progressBar: true,
    }),
    importProvidersFrom(NgxSpinnerModule)         
  ]
};

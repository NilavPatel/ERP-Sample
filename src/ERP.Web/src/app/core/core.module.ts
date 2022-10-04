import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AuthGuard } from './guards/auth.guard';
import { PermissionGuard } from './guards/permission.guard';
import { HttpCustomInterceptor } from './interceptors/http.interceptors';
import { AuthenticationService } from './services/authentication.service';
import { LoaderService } from './services/loader.service';
import { LoginService } from './services/login.service';
import { MenuService } from './services/menu.service';
import { PermissionService } from './services/permission.service';

@NgModule({
  declarations: [],
  imports: [HttpClientModule],
  exports: [HttpClientModule],
  providers: [
    // interceptors
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpCustomInterceptor,
      multi: true
    },

    // guards
    AuthGuard,
    PermissionGuard,

    // services
    AuthenticationService,
    LoaderService,
    PermissionService,
    MenuService,

    // http services
    LoginService, // Added here because intereptor having dependency on login service

    // primeng services
    MessageService // service added here instead of primeng.module.ts because it needs to be singleton during app
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}

export function throwIfAlreadyLoaded(parentModule: any, moduleName: string) {
  if (parentModule) {
    throw new Error(
      `${moduleName} has already been loaded. Import ${moduleName} modules in the AppModule only.`
    );
  }
}

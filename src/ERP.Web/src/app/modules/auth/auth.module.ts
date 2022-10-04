import { NgModule } from '@angular/core';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    AuthRoutingModule,
    SharedModule
  ]
})
export class AuthModule { }

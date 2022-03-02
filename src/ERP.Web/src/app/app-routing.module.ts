import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { ThemeComponent } from './layout/theme/theme.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'app',
    component: ThemeComponent,
    canActivate: [AuthGuard],
    children: [{
      path: 'dashboard',
      loadChildren: () => import('./modules/dashboard/dashboard.module').then(m => m.DashboardModule)
    },
    {
      path: 'employee',
      loadChildren: () => import('./modules/employee/employee.module').then(m => m.EmployeeModule)
    },
    {
      path: 'user',
      loadChildren: () => import('./modules/user/user.module').then(m => m.UserModule)
    },
    {
      path: 'role',
      loadChildren: () => import('./modules/role/role.module').then(m => m.RoleModule)
    },
    {
      path: 'designation',
      loadChildren: () => import('./modules/designation/designation.module').then(m => m.DesignationModule)
    }]
  },
  {
    path: '',
    redirectTo: 'auth',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'auth',
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

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
      path: 'employees',
      loadChildren: () => import('./modules/employees/employees.module').then(m => m.EmployeesModule)
    },
    {
      path: 'users',
      loadChildren: () => import('./modules/users/users.module').then(m => m.UsersModule)
    },
    {
      path: 'roles',
      loadChildren: () => import('./modules/roles/roles.module').then(m => m.RolesModule)
    },
    {
      path: 'designations',
      loadChildren: () => import('./modules/designations/designations.module').then(m => m.DesignationsModule)
    },
    {
      path: 'departments',
      loadChildren: () => import('./modules/departments/departments.module').then(m => m.DepartmentsModule)
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

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { ThemeComponent } from './shared/components/theme/theme.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  }, {
    path: 'app',
    component: ThemeComponent,
    canActivate: [AuthGuard],
    children: [{
      path: 'dashboard',
      loadChildren: () => import('./modules/dashboards/dashboards.module').then(m => m.DashboardsModule)
    }, {
      path: 'employees',
      loadChildren: () => import('./modules/employees/employees.module').then(m => m.EmployeesModule)
    }, {
      path: 'users',
      loadChildren: () => import('./modules/users/users.module').then(m => m.UsersModule)
    }, {
      path: 'roles',
      loadChildren: () => import('./modules/roles/roles.module').then(m => m.RolesModule)
    }, {
      path: 'designations',
      loadChildren: () => import('./modules/designations/designations.module').then(m => m.DesignationsModule)
    }, {
      path: 'leave-types',
      loadChildren: () => import('./modules/leave-types/leave-types.module').then(m => m.LeaveTypesModule)
    }, {
      path: 'departments',
      loadChildren: () => import('./modules/departments/departments.module').then(m => m.DepartmentsModule)
    }, {
      path: 'holidays',
      loadChildren: () => import('./modules/holidays/holidays.module').then(m => m.HolidaysModule)
    }]
  }, {
    path: '',
    redirectTo: 'auth',
    pathMatch: 'full'
  }, {
    path: '**',
    redirectTo: 'auth',
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

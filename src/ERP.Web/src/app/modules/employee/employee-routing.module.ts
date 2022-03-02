import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionEnum } from 'src/app/core/enums/permission.enum';
import { PermissionGuard } from 'src/app/core/guards/permission.guard';
import { EmployeeAddComponent } from './employee-add/employee-add.component';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeViewComponent } from './employee-view/employee-view.component';

const routes: Routes = [{
  path: "list",
  component: EmployeeListComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.EmployeeView }
},
{
  path: "add",
  component: EmployeeAddComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.EmployeeAdd }
},
{
  path: "edit/:id",
  component: EmployeeEditComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.EmployeeEdit }
},
{
  path: "view/:id",
  component: EmployeeViewComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.EmployeeView }
},
{
  path: '',
  redirectTo: 'list',
  pathMatch: 'full'
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionEnum } from 'src/app/core/enums/permission.enum';
import { PermissionGuard } from 'src/app/core/guards/permission.guard';
import { DepartmentAddComponent } from './department-add/department-add.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';
import { DepartmentListComponent } from './department-list/department-list.component';
import { DepartmentViewComponent } from './department-view/department-view.component';

const routes: Routes = [{
  path: 'list',
  component: DepartmentListComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DepartmentView }
},
{
  path: 'add',
  component: DepartmentAddComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DepartmentAdd }
},
{
  path: 'edit/:id',
  component: DepartmentEditComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DepartmentEdit }
},
{
  path: 'view/:id',
  component: DepartmentViewComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DepartmentView }
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
export class DepartmentsRoutingModule { }

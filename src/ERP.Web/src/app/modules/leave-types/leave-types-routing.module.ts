import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { PermissionGuard } from 'src/app/core/guards/permission.guard';
import { LeaveTypeAddComponent } from './leave-type-add/leave-type-add.component';
import { LeaveTypeEditComponent } from './leave-type-edit/leave-type-edit.component';
import { LeaveTypeListComponent } from './leave-type-list/leave-type-list.component';

const routes: Routes = [{
  path: 'list',
  component: LeaveTypeListComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.LeaveTypeView }
}, {
  path: 'add',
  component: LeaveTypeAddComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.LeaveTypeAdd }
}, {
  path: 'edit/:id',
  component: LeaveTypeEditComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.LeaveTypeEdit }
}, {
  path: '',
  redirectTo: 'list',
  pathMatch: 'full'
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveTypesRoutingModule { }

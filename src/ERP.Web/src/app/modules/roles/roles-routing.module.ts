import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { PermissionGuard } from 'src/app/core/guards/permission.guard';
import { RoleAddComponent } from './role-add/role-add.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { RoleListComponent } from './role-list/role-list.component';
import { RoleViewComponent } from './role-view/role-view.component';

const routes: Routes = [{
  path: 'list',
  component: RoleListComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.RoleView }
}, {
  path: 'add',
  component: RoleAddComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.RoleAdd }
}, {
  path: 'edit/:id',
  component: RoleEditComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.RoleEdit }
}, {
  path: 'view/:id',
  component: RoleViewComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.RoleView }
}, {
  path: '',
  redirectTo: 'list',
  pathMatch: 'full'
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RolesRoutingModule { }

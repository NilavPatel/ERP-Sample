import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionEnum } from 'src/app/core/enums/permission.enum';
import { PermissionGuard } from 'src/app/core/guards/permission.guard';
import { UserAddComponent } from './user-add/user-add.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserViewComponent } from './user-view/user-view.component';

const routes: Routes = [{
  path: "list",
  component: UserListComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.UserView }
},
{
  path: "add",
  component: UserAddComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.UserAdd }
},
{
  path: "edit/:id",
  component: UserEditComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.UserEdit }
},
{
  path: "view/:id",
  component: UserViewComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.UserView }
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
export class UserRoutingModule { }

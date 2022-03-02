import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionEnum } from 'src/app/core/enums/permission.enum';
import { PermissionGuard } from 'src/app/core/guards/permission.guard';
import { DesignationAddComponent } from './designation-add/designation-add.component';
import { DesignationEditComponent } from './designation-edit/designation-edit.component';
import { DesignationListComponent } from './designation-list/designation-list.component';
import { DesignationViewComponent } from './designation-view/designation-view.component';

const routes: Routes = [{
  path: 'list',
  component: DesignationListComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DesignationView }
},
{
  path: 'add',
  component: DesignationAddComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DesignationAdd }
},
{
  path: 'edit/:id',
  component: DesignationEditComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DesignationEdit }
},
{
  path: 'view/:id',
  component: DesignationViewComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DesignationView }
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
export class DesignationRoutingModule { }

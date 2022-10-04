import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { PermissionGuard } from 'src/app/core/guards/permission.guard';
import { DesignationAddComponent } from './designation-add/designation-add.component';
import { DesignationEditComponent } from './designation-edit/designation-edit.component';
import { DesignationListComponent } from './designation-list/designation-list.component';

const routes: Routes = [{
  path: 'list',
  component: DesignationListComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DesignationView }
}, {
  path: 'add',
  component: DesignationAddComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DesignationAdd }
}, {
  path: 'edit/:id',
  component: DesignationEditComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.DesignationEdit }
}, {
  path: '',
  redirectTo: 'list',
  pathMatch: 'full'
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DesignationsRoutingModule { }

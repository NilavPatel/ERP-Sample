import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { PermissionGuard } from 'src/app/core/guards/permission.guard';
import { HolidayAddComponent } from './holiday-add/holiday-add.component';
import { HolidayEditComponent } from './holiday-edit/holiday-edit.component';
import { HolidayListComponent } from './holiday-list/holiday-list.component';

const routes: Routes = [{
  path: 'list',
  component: HolidayListComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.HolidayView }
}, {
  path: 'add',
  component: HolidayAddComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.HolidayAdd }
}, {
  path: 'edit/:id',
  component: HolidayEditComponent,
  canActivate: [PermissionGuard],
  data: { permission: PermissionEnum.HolidayEdit }
}, {
  path: '',
  redirectTo: 'list',
  pathMatch: 'full'
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HolidaysRoutingModule { }

import { NgModule } from '@angular/core';
import { EmployeeEducationListComponent } from './employee-education-list/employee-education-list.component';
import { EmployeeEducationAddComponent } from './employee-education-add/employee-education-add.component';
import { EmployeeEducationEditComponent } from './employee-education-edit/employee-education-edit.component';
import { PermissionService } from 'src/app/core/services/permission.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { EmployeeEducationService } from './shared/employee-education.service';

@NgModule({
  declarations: [
    EmployeeEducationListComponent,
    EmployeeEducationAddComponent,
    EmployeeEducationEditComponent
  ],
  imports: [
    SharedModule
  ],
  providers: [
    EmployeeEducationService,
    PermissionService,
    LoaderService
  ],
  exports: [
    EmployeeEducationListComponent,
    EmployeeEducationAddComponent,
    EmployeeEducationEditComponent
  ]
})
export class EmployeeEducationModule { }

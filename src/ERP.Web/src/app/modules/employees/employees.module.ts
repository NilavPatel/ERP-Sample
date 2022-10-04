import { NgModule } from '@angular/core';
import { EmployeesRoutingModule } from './employees-routing.module';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeService } from 'src/app/modules/employees/shared/employee.service';
import { EmployeeAddComponent } from './employee-add/employee-add.component';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { EmployeeViewComponent } from './employee-view/employee-view.component';
import { DesignationService } from 'src/app/modules/designations/shared/designation.service';
import { DepartmentService } from 'src/app/modules/departments/shared/department.service';
import { EmployeeEducationModule } from '../employee-education/employee-education.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeAddComponent,
    EmployeeEditComponent,
    EmployeeViewComponent
  ],
  imports: [
    EmployeesRoutingModule,
    EmployeeEducationModule,
    SharedModule
  ],
  providers: [
    EmployeeService,
    DesignationService,
    DepartmentService
  ]
})
export class EmployeesModule { }

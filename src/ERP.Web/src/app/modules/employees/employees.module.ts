import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpCustomInterceptor } from 'src/app/core/interceptors/http.interceptors';
import { FormsModule } from '@angular/forms';

import { EmployeesRoutingModule } from './employees-routing.module';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeService } from '../../core/services/employee.service';
import { EmployeeAddComponent } from './employee-add/employee-add.component';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { EmployeeViewComponent } from './employee-view/employee-view.component';
import { DesignationService } from 'src/app/core/services/designation.service';

import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TooltipModule } from 'primeng/tooltip';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CalendarModule } from 'primeng/calendar';
import { TabViewModule } from 'primeng/tabview';
import { FileUploadModule } from 'primeng/fileupload';
import { RadioButtonModule } from 'primeng/radiobutton';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DropdownModule } from 'primeng/dropdown';
import { AvatarModule } from 'primeng/avatar';
import { DepartmentService } from 'src/app/core/services/department.service';

@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeAddComponent,
    EmployeeEditComponent,
    EmployeeViewComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    EmployeesRoutingModule,
    PaginatorModule,
    TableModule,
    ToolbarModule,
    ButtonModule,
    InputTextModule,
    TooltipModule,
    CardModule,
    CheckboxModule,
    InputTextareaModule,
    CalendarModule,
    TabViewModule,
    FileUploadModule,
    RadioButtonModule,
    AutoCompleteModule,
    DropdownModule,
    AvatarModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpCustomInterceptor,
    multi: true
  }, EmployeeService,
    DesignationService,
    DepartmentService]
})
export class EmployeesModule { }

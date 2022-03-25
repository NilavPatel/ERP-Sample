import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpCustomInterceptor } from 'src/app/core/interceptors/http.interceptors';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { DepartmentsRoutingModule } from './departments-routing.module';
import { DepartmentViewComponent } from './department-view/department-view.component';
import { DepartmentListComponent } from './department-list/department-list.component';
import { DepartmentAddComponent } from './department-add/department-add.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';
import { DepartmentService } from 'src/app/core/services/department.service';

import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { TooltipModule } from 'primeng/tooltip';
import { PaginatorModule } from 'primeng/paginator';

@NgModule({
  declarations: [
    DepartmentViewComponent,
    DepartmentListComponent,
    DepartmentAddComponent,
    DepartmentEditComponent
  ],
  imports: [
    CommonModule,
    DepartmentsRoutingModule,
    HttpClientModule,
    FormsModule,
    PaginatorModule,
    TableModule,
    ToolbarModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule,
    TooltipModule,
    CardModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpCustomInterceptor,
    multi: true
  }, DepartmentService]
})
export class DepartmentsModule { }

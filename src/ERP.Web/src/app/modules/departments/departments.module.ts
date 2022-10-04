import { NgModule } from '@angular/core';
import { DepartmentsRoutingModule } from './departments-routing.module';
import { DepartmentListComponent } from './department-list/department-list.component';
import { DepartmentAddComponent } from './department-add/department-add.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';
import { DepartmentService } from 'src/app/modules/departments/shared/department.service';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    DepartmentListComponent,
    DepartmentAddComponent,
    DepartmentEditComponent
  ],
  imports: [
    DepartmentsRoutingModule,
    SharedModule
  ],
  providers: [DepartmentService]
})
export class DepartmentsModule { }

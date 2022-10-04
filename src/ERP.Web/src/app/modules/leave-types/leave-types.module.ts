import { NgModule } from '@angular/core';
import { LeaveTypesRoutingModule } from './leave-types-routing.module';
import { LeaveTypeAddComponent } from './leave-type-add/leave-type-add.component';
import { LeaveTypeEditComponent } from './leave-type-edit/leave-type-edit.component';
import { LeaveTypeListComponent } from './leave-type-list/leave-type-list.component';
import { LeaveTypeService } from 'src/app/modules/leave-types/shared/leave-type.service';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    LeaveTypeAddComponent,
    LeaveTypeEditComponent,
    LeaveTypeListComponent
  ],
  imports: [
    LeaveTypesRoutingModule,
    SharedModule
  ],
  providers: [
    LeaveTypeService
  ]
})
export class LeaveTypesModule { }

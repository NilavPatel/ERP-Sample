import { NgModule } from '@angular/core';
import { DesignationsRoutingModule } from './designations-routing.module';
import { DesignationListComponent } from './designation-list/designation-list.component';
import { DesignationService } from 'src/app/modules/designations/shared/designation.service';
import { DesignationAddComponent } from './designation-add/designation-add.component';
import { DesignationEditComponent } from './designation-edit/designation-edit.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    DesignationListComponent,
    DesignationAddComponent,
    DesignationEditComponent
  ],
  imports: [
    DesignationsRoutingModule,
    SharedModule
  ],
  providers: [DesignationService]
})
export class DesignationsModule { }

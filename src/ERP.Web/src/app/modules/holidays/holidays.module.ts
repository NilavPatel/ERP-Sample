import { NgModule } from '@angular/core';
import { HolidaysRoutingModule } from './holidays-routing.module';
import { HolidayListComponent } from './holiday-list/holiday-list.component';
import { HolidayAddComponent } from './holiday-add/holiday-add.component';
import { HolidayEditComponent } from './holiday-edit/holiday-edit.component';
import { HolidayService } from 'src/app/modules/holidays/shared/holiday.service';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    HolidayListComponent,
    HolidayAddComponent,
    HolidayEditComponent
  ],
  imports: [
    HolidaysRoutingModule,
    SharedModule
  ],
  providers: [
    HolidayService
  ]
})
export class HolidaysModule { }

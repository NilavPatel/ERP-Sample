import { NgModule } from '@angular/core';
import { DashboardsRoutingModule } from './dashboards-routing.module';
import { HolidayService } from 'src/app/modules/holidays/shared/holiday.service';
import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import { EmployeeService } from 'src/app/modules/employees/shared/employee.service';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { DashboardService } from 'src/app/modules/dashboards/shared/dashboard.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { GeneralDashboardComponent } from './general-dashboard/general-dashboard.component';

FullCalendarModule.registerPlugins([
  dayGridPlugin
]);

@NgModule({
  declarations: [
    GeneralDashboardComponent
  ],
  imports: [
    DashboardsRoutingModule,
    FullCalendarModule,
    SharedModule
  ],
  providers: [
    HolidayService,
    EmployeeService,
    AuthenticationService,
    DashboardService
  ]
})
export class DashboardsModule { }

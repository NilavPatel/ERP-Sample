import { Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { HolidayService } from 'src/app/modules/holidays/shared/holiday.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { CalendarOptions, FullCalendarComponent } from '@fullcalendar/angular';
import { EmployeeService } from 'src/app/modules/employees/shared/employee.service';
import { environment } from "src/environments/environment";
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { DashboardService } from 'src/app/modules/dashboards/shared/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './general-dashboard.component.html',
  styleUrls: ['./general-dashboard.component.scss']
})
export class GeneralDashboardComponent implements OnInit {
  currentYear: number = new Date().getFullYear();
  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    height: 500,
    customButtons: {
      next: {
        click: this.onNextMonthClick.bind(this)
      },
      prev: {
        click: this.onPrevMonthClick.bind(this),
      },
      today: {
        text: "Today",
        click: this.onTodayClick.bind(this),
      },
    }
  };

  profilePhotoName: string | null = null;
  employeeCode: string = "";
  name: string = "";
  designation: string | null = "";
  reportingTo: string | null = "";
  department: string | null = "";
  birthdays: any[] = [];

  @ViewChild('calendar') calendarComponent!: FullCalendarComponent;

  constructor(private holidayService: HolidayService,
    private messageService: MessageService,
    private employeeService: EmployeeService,
    private loaderService: LoaderService,
    private authenticationService: AuthenticationService,
    private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.getHolidaysWithYear(this.currentYear);
    this.getCurrentEmployeeDetails();
    this.getBirthdays();
  }

  getHolidaysWithYear(year: number) {
    this.loaderService.showLoader();
    var req = {
      year: year,
    };
    this.holidayService.getAllHolidayByYear(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          var events: any[] = [];
          if (value.data && value.data.length > 0) {
            value.data.forEach((element: any) => {
              events.push({
                title: element.name,
                date: new Date(element.holidayOn),
                allDay: true
              });
            });
          }
          this.calendarComponent.getApi().addEventSource({
            id: "holidays",
            events: events,
            className: "bg-blue-500 border-blue-500"
          });
        } else {
          this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
        }
      },
      error: (error: any) => {
        this.messageService.add({ severity: 'error', detail: error.message });
        this.loaderService.hideLoader();
      }, complete: () => {
        this.loaderService.hideLoader();
      }
    });
  }

  onNextMonthClick(event: any) {
    this.calendarComponent.getApi().next();
    this.fetchHolidays();
  }

  onPrevMonthClick(event: any) {
    this.calendarComponent.getApi().prev();
    this.fetchHolidays();
  }

  onTodayClick(event: any) {
    this.calendarComponent.getApi().today();
    this.fetchHolidays();
  }

  fetchHolidays() {
    var currentYear = this.calendarComponent.getApi().getDate().getFullYear();
    if (this.currentYear != currentYear) {
      this.calendarComponent.getApi().getEventSourceById("holidays")?.remove();
      this.currentYear = currentYear;
      this.getHolidaysWithYear(this.currentYear);
    }
  }

  getCurrentEmployeeDetails() {
    this.loaderService.showLoader();
    this.employeeService.getLoginEmployeeDetails().subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          this.profilePhotoName = value.data.profilePhotoName;
          this.employeeCode = value.data.employeeCode;
          this.name = value.data.firstName + " " + value.data.middleName + " " + value.data.lastName;
          this.designation = value.data.designationName;
          this.reportingTo = value.data.reportingToName;
          this.department = value.data.departmentName;
        } else {
          this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
        }
      },
      error: (error: any) => {
        this.messageService.add({ severity: 'error', detail: error.message });
        this.loaderService.hideLoader();
      }, complete: () => {
        this.loaderService.hideLoader();
      }
    });
  }

  getProfilePhotoPath() {
    var token = this.authenticationService.getCurrentUser().token;
    return environment.apiURL + "/Employee/GetEmployeeProfilePhoto?photoName=" + this.profilePhotoName
      + "&token=" + token;
  }

  getBirthdays() {
    this.loaderService.showLoader();
    this.dashboardService.getWeeklyBirthdays().subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data && value.data.employeeBirthdays && value.data.employeeBirthdays.length > 0) {
            this.birthdays = value.data.employeeBirthdays;
          }
        } else {
          this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
        }
      },
      error: (error: any) => {
        this.messageService.add({ severity: 'error', detail: error.message });
        this.loaderService.hideLoader();
      }, complete: () => {
        this.loaderService.hideLoader();
      }
    });
  }
}

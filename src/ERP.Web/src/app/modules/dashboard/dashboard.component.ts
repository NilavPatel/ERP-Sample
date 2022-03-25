import { Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { CalendarOptions, FullCalendarComponent } from '@fullcalendar/angular';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { environment } from "../../../environments/environment";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
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
  bloodGroup: string | null = "";
  designation: string | null = "";
  reportingTo: string | null = "";
  department: string | null = "";

  @ViewChild('calendar') calendarComponent!: FullCalendarComponent;

  constructor(private messageService: MessageService,
    private employeeService: EmployeeService,
    private loaderService: LoaderService) { }

  ngOnInit(): void {
    this.getCurrentEmployeeDetails();
  }

  onNextMonthClick(event: any) {
    this.calendarComponent.getApi().next();
  }

  onPrevMonthClick(event: any) {
    this.calendarComponent.getApi().prev();
  }

  onTodayClick(event: any) {
    this.calendarComponent.getApi().today();
  }

  getCurrentEmployeeDetails() {
    this.loaderService.showLoader();
    this.employeeService.getLoginEmployeeDetails().subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          this.profilePhotoName = value.data.profilePhotoName;
          this.employeeCode = value.data.employeeCode;
          this.name = value.data.firstName + " " + value.data.middleName + " " + value.data.lastName;
          this.bloodGroup = value.data.bloodGroup;
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
    return environment.apiURL + "/Employee/GetEmployeeProfilePhoto?photoName=" + this.profilePhotoName;
  }
}

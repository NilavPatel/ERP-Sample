import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { EmployeeService } from 'src/app/modules/employees/shared/employee.service';
import { environment } from "src/environments/environment";

@Component({
  selector: 'app-employee-view',
  templateUrl: './employee-view.component.html',
  styleUrls: ['./employee-view.component.scss']
})
export class EmployeeViewComponent implements OnInit {

  id: string | null = "";
  employeeCode: string = "";
  firstName: string = "";
  middleName: string = "";
  lastName: string = "";
  fullName: String = "";
  officeEmailId: string | null = null;
  officeContactNo: string | null = null;
  joiningOn: any = new Date();
  confirmationOn: any = null;
  resignationOn: any = null;
  relievingOn: any = null;
  designationName: string | null = null;
  departmentName: string | null = null;
  reportingToName: string | null = null;
  profilePhotoName: string | null = null;

  birthDate: any = null;
  bloodGroup: string | null = "";
  genderText: string = "";
  parmenantAddress: string | null = null;
  currentAddress: string | null = null;
  isCurrentSameAsParmenantAddress: boolean = false;
  maritalStatusText: string | null = null;
  personalEmailId: string | null = null;
  personalMobileNo: string | null = null;
  otherContactNo: string | null = null;

  bankName: string | null = null;
  ifscCode: string | null = null;
  branchAddress: string | null = null;
  accountNumber: string | null = null;
  panNumber: string | null = null;
  pfNumber: string | null = null;
  uanNumber: string | null = null;

  files: any[] = [];

  constructor(private router: Router,
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private authenticationService: AuthenticationService,
    private loaderService: LoaderService,
    private messageService: MessageService,) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getEmployeeDetails();
    }
  }

  onChangeTab($event: any) {
    switch ($event.index) {
      case 0:
        this.getEmployeeDetails();
        break;
      case 1:
        this.getEmployeePersonalDetail();
        break;
      case 2:
        this.getEmployeeBankDetail();
        break;
      case 3:
        this.getEmployeeDocuments();
        break;
    }
  }

  goToList() {
    this.router.navigateByUrl("/app/employees/list");
  }

  getEmployeeDetails() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.employeeService.getEmployeeById(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            if (value.data) {
              this.employeeCode = value.data.employeeCode;
              this.firstName = value.data.firstName;
              this.middleName = value.data.middleName;
              this.lastName = value.data.lastName;
              this.fullName = this.firstName + " " + this.middleName + " " + this.lastName;
              this.officeEmailId = value.data.officeEmailId;
              this.officeContactNo = value.data.officeContactNo;
              this.joiningOn = value.data.joiningOn && value.data.joiningOn.length > 0 ? new Date(value.data.joiningOn) : null;
              this.confirmationOn = value.data.confirmationOn && value.data.confirmationOn.length > 0 ? new Date(value.data.confirmationOn) : null;
              this.resignationOn = value.data.resignationOn && value.data.resignationOn.length > 0 ? new Date(value.data.resignationOn) : null;
              this.relievingOn = value.data.relievingOn && value.data.relievingOn.length > 0 ? new Date(value.data.relievingOn) : null;
              this.designationName = value.data.designationName;
              this.departmentName = value.data.departmentName;
              this.reportingToName = value.data.reportingToName;
              this.profilePhotoName = value.data.profilePhotoName;
            }
          } else {
            this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
          }
        }, error: (error: any) => {
          this.messageService.add({ severity: 'error', detail: error.message });
          this.loaderService.hideLoader();
        }, complete: () => {
          this.loaderService.hideLoader();
        }
      });
  }

  getEmployeePersonalDetail() {
    this.loaderService.showLoader();
    var req = {
      employeeId: this.id
    };
    this.employeeService.getEmployeePersonalDetailById(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            if (value.data) {
              this.birthDate = value.data.birthDate && value.data.birthDate.length > 0 ? new Date(value.data.birthDate) : null;
              this.bloodGroup = value.data.bloodGroup;
              this.genderText = value.data.genderText;
              this.parmenantAddress = value.data.parmenantAddress;
              this.currentAddress = value.data.currentAddress;
              this.isCurrentSameAsParmenantAddress = value.data.isCurrentSameAsParmenantAddress;
              this.maritalStatusText = value.data.maritalStatusText;
              this.personalEmailId = value.data.personalEmailId;
              this.personalMobileNo = value.data.personalMobileNo;
              this.otherContactNo = value.data.otherContactNo;
            }
          } else {
            this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
          }
        }, error: (error: any) => {
          this.messageService.add({ severity: 'error', detail: error.message });
          this.loaderService.hideLoader();
        }, complete: () => {
          this.loaderService.hideLoader();
        }
      });
  }

  getEmployeeBankDetail() {
    this.loaderService.showLoader();
    var req = {
      employeeId: this.id
    };
    this.employeeService.getEmployeeBankDetail(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            if (value.data) {
              this.bankName = value.data.bankName;
              this.ifscCode = value.data.ifscCode;
              this.branchAddress = value.data.branchAddress;
              this.accountNumber = value.data.accountNumber;
              this.panNumber = value.data.panNumber;
              this.pfNumber = value.data.pfNumber;
              this.uanNumber = value.data.uanNumber;
            }
          } else {
            this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
          }
        }, error: (error: any) => {
          this.messageService.add({ severity: 'error', detail: error.message });
          this.loaderService.hideLoader();
        }, complete: () => {
          this.loaderService.hideLoader();
        }
      });
  }

  getEmployeeDocuments() {
    this.loaderService.showLoader();
    var req = {
      employeeId: this.id
    };
    this.employeeService.getEmployeeDocumentsById(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            if (value.data) {
              this.files = value.data;
            } else {
              this.files = [];
            }
          } else {
            this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
          }
        }, error: (error: any) => {
          this.messageService.add({ severity: 'error', detail: error.message });
          this.loaderService.hideLoader();
        }, complete: () => {
          this.loaderService.hideLoader();
        }
      });
  }

  downloadEmployeeDocument(docId: any) {
    var token = this.authenticationService.getCurrentUser().token;
    this.employeeService.downloadEmployeeDocument(docId, token);
  }

  getProfilePhotoPath() {
    var token = this.authenticationService.getCurrentUser().token;
    return environment.apiURL + "/Employee/GetEmployeeProfilePhoto?photoName=" + this.profilePhotoName
      + "&token=" + token;
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { environment } from "../../../../environments/environment";

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
  officeEmailId: string | null = null;
  officeContactNo: string | null = null;
  joiningOn: any = new Date();
  relievingOn: any = null;
  designationName: string | null = null;
  departmentName: string | null = null;
  reportingToName: string | null = null;
  profilePhotoName: string | null = null;

  bankName: string | null = null;
  ifscCode: string | null = null;
  branchAddress: string | null = null;
  accountNumber: string | null = null;
  panNumber: string | null = null;

  files: any[] = [];

  constructor(private router: Router,
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private authenticationService: AuthenticationService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

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
        this.getEmployeeDetails();
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
              this.officeEmailId = value.data.officeEmailId;
              this.officeContactNo = value.data.officeContactNo;
              this.joiningOn = value.data.joiningOn && value.data.joiningOn.length > 0 ? new Date(value.data.joiningOn) : null;
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
    return environment.apiURL + "/Employee/GetEmployeeProfilePhoto?photoName=" + this.profilePhotoName;
  }
}

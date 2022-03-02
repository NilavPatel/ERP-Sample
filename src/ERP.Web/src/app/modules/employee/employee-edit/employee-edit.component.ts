import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FileUpload } from 'primeng/fileupload';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { DesignationService } from 'src/app/core/services/designation.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { EmployeeService } from '../../../core/services/employee.service';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.scss']
})
export class EmployeeEditComponent implements OnInit {

  id: string | null = "";
  employeeCode: string = "";
  firstName: string = "";
  middleName: string = "";
  lastName: string = "";
  birthDate: any = null;
  bloodGroup: string | null = "";
  gender: number = 0;
  parmenantAddress: string | null = null;
  currentAddress: string | null = null;
  isCurrentSameAsParmenantAddress: boolean = false;
  maritalStatus: number | null = null;
  personalEmailId: string | null = null;
  personalMobileNo: string | null = null;
  otherContactNo: string | null = null;
  officeEmailId: string | null = null;
  officeContactNo: string | null = null;
  joiningOn: any = new Date();
  relievingOn: any = null;
  designationId: string | null = null;
  reportingTo: any = null;

  bankDetailsId: string | null = null;
  bankName: string | null = null;
  ifscCode: string | null = null;
  branchAddress: string | null = null;
  accountNumber: string | null = null;
  panNumber: string | null = null;

  isFileNotSelected: boolean = false;
  uploadedFile: File | null = null;
  description: string = "";
  files: any[] = [];

  bloodGroups: string[] = [];
  designations: any[] = [];
  reportingToEmployees: any[] = [];
  searchReportingPerson: string = "";

  constructor(private router: Router,
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private authenticationService: AuthenticationService,
    private loaderService: LoaderService,
    private designationService: DesignationService,
    private messageService: MessageService) {
    this.bloodGroups = [
      'A+', 'A-', 'B+', 'B-', 'O+', 'O-', 'AB+', 'AB-'
    ];
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getEmployeeDetails();
      this.getDesignationList();
    }
  }

  onChangeTab($event: any) {
    switch ($event.index) {
      case 0:
        this.getEmployeeDetails();
        break;
      case 1:
        this.getEmployeeBankDetail();
        break;
      case 2:
        this.getEmployeeDocuments();
        break;
    }
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
              this.gender = value.data.gender;
              this.parmenantAddress = value.data.parmenantAddress;
              this.currentAddress = value.data.currentAddress;
              this.isCurrentSameAsParmenantAddress = value.data.isCurrentSameAsParmenantAddress;
              this.maritalStatus = value.data.maritalStatus;
              this.personalEmailId = value.data.personalEmailId;
              this.personalMobileNo = value.data.personalMobileNo;
              this.otherContactNo = value.data.otherContactNo;
              this.officeEmailId = value.data.officeEmailId;
              this.officeContactNo = value.data.officeContactNo;
              this.joiningOn = value.data.joiningOn && value.data.joiningOn.length > 0 ? new Date(value.data.joiningOn) : null;
              this.relievingOn = value.data.relievingOn && value.data.relievingOn.length > 0 ? new Date(value.data.relievingOn) : null;
              this.designationId = value.data.designationId;
              this.reportingTo = {
                id: value.data.reportingToId,
                fullName: value.data.reportingToName
              };
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
              this.bankDetailsId = value.data.id;
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
            this.files = value.data;
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

  getDesignationList() {
    this.loaderService.showLoader();
    var req = {
      searchKeyword: "",
      pageIndex: 0,
      pageSize: 100
    };
    this.designationService.getAllDesignations(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.designations = value.data.result;
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

  getAvailableReportingPersons(event: any) {
    this.loaderService.showLoader();
    var req = {
      employeeId: this.id,
      searchKeyword: event.query,
      pageIndex: 0,
      pageSize: 50
    };
    this.employeeService.getAvailableReportingPersons(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            value.data.forEach((element: any) => {
              element.fullName = element.firstName + " " + element.lastName + " (" + element.designationName + ")";
            });
            this.reportingToEmployees = value.data;
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

  goToList() {
    this.router.navigateByUrl("/app/employee/list");
  }

  onChangeSameAsAddress() {
    if (this.isCurrentSameAsParmenantAddress) {
      this.currentAddress = null;
    }
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      id: this.id,
      firstName: this.firstName,
      middleName: this.middleName,
      lastName: this.lastName,
      birthDate: this.birthDate,
      bloodGroup: this.bloodGroup,
      gender: this.gender,
      parmenantAddress: this.parmenantAddress,
      currentAddress: this.currentAddress,
      isCurrentSameAsParmenantAddress: this.isCurrentSameAsParmenantAddress,
      maritalStatus: this.maritalStatus,
      personalEmailId: this.personalEmailId,
      personalMobileNo: this.personalMobileNo,
      otherContactNo: this.otherContactNo,
      officeEmailId: this.officeEmailId,
      officeContactNo: this.officeContactNo,
      joiningOn: this.joiningOn,
      relievingOn: this.relievingOn,
      designationId: this.designationId,
      reportingToId: this.reportingTo == null ? null : this.reportingTo.id
    }
    this.employeeService.updateEmployee(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.getEmployeeDetails();
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
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

  onSubmitBankDetails() {
    this.loaderService.showLoader();
    var req = {
      employeeId: this.id,
      bankName: this.bankName,
      ifscCode: this.ifscCode,
      branchAddress: this.branchAddress,
      accountNumber: this.accountNumber,
      panNumber: this.panNumber
    }
    if (this.bankDetailsId) {
      this.employeeService.updateEmployeeBankDetail(req)
        .subscribe({
          next: (value: any) => {
            if (value && value.isValid) {
              this.getEmployeeBankDetail();
              this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
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
    } else {
      this.employeeService.addEmployeeBankDetail(req)
        .subscribe({
          next: (value: any) => {
            if (value && value.isValid) {
              this.getEmployeeBankDetail();
              this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
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
  }

  onSelectFile(event: any) {
    if (!event.currentFiles || !event.currentFiles[0]) {
      this.isFileNotSelected = true;
      this.uploadedFile = null;
      return;
    }
    this.isFileNotSelected = false;
    this.uploadedFile = event.currentFiles[0];
  }

  onUploadFile(docForm: NgForm, fileUpload: FileUpload) {
    if (!this.uploadedFile) {
      this.isFileNotSelected = true;
      return;
    }
    this.loaderService.showLoader();
    const formData: FormData = new FormData();
    formData.append('Document', this.uploadedFile!, this.uploadedFile!.name);
    formData.append('EmployeeId', this.id!);
    formData.append('Description', this.description);
    this.employeeService.uploadEmployeeDocument(formData)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.uploadedFile = null;
            this.description = "";
            fileUpload.clear();
            docForm.resetForm();
            this.getEmployeeDocuments();
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

  onClearFile(docForm: NgForm, fileUpload: FileUpload) {
    this.uploadedFile = null;
    this.description = "";
    this.isFileNotSelected = false;
    fileUpload.clear();
    docForm.resetForm();
  }

  removeEmployeeDocument(docId: any) {
    this.loaderService.showLoader();
    var req = {
      id: docId,
      employeeId: this.id
    };
    this.employeeService.removeEmployeeDocument(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.getEmployeeDocuments();
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
}

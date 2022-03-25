import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { EmployeeService } from '../../../core/services/employee.service';

@Component({
  selector: 'app-employee-add',
  templateUrl: './employee-add.component.html',
  styleUrls: ['./employee-add.component.scss']
})
export class EmployeeAddComponent implements OnInit {

  employeeCode: string = "";
  firstName: string = "";
  middleName: string = "";
  lastName: string = "";
  birthDate: any = null;
  gender: number = 0;
  parmenantAddress: string | null = null;
  currentAddress: string | null = null;
  isCurrentSameAsParmenantAddress: boolean = false;
  personalEmailId: string | null = null;
  personalMobileNo: string | null = null;
  otherContactNo: string | null = null;
  joiningOn: any = new Date();
  today: any = new Date();

  constructor(private router: Router,
    private employeeService: EmployeeService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
  }

  goToList() {
    this.router.navigateByUrl("/app/employees/list");
  }

  onClearClick(form: NgForm) {
    this.employeeCode = "";
    this.firstName = "";
    this.lastName = "";
    this.middleName = "";
    this.birthDate = null;
    this.parmenantAddress = null;
    this.currentAddress = null;
    this.personalEmailId = null;
    this.personalMobileNo = null;
    this.otherContactNo = null;

    form.resetForm();
    this.joiningOn = new Date();
    this.gender = 0;
    this.isCurrentSameAsParmenantAddress = false;
  }

  onChangeSameAsAddress() {
    if (this.isCurrentSameAsParmenantAddress) {
      this.currentAddress = null;
    }
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      employeeCode: this.employeeCode,
      firstName: this.firstName,
      lastName: this.lastName,
      middleName: this.middleName,
      birthDate: this.birthDate,
      gender: this.gender,
      parmenantAddress: this.parmenantAddress,
      currentAddress: this.currentAddress,
      isCurrentSameAsParmenantAddress: this.isCurrentSameAsParmenantAddress,
      personalEmailId: this.personalEmailId,
      personalMobileNo: this.personalMobileNo,
      otherContactNo: this.otherContactNo,
      joiningOn: this.joiningOn
    }
    this.employeeService.createEmployee(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.goToList();
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

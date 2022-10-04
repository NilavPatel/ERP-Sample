import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { LoaderService } from 'src/app/core/services/loader.service';
import { EmployeeEducationService } from '../shared/employee-education.service';

@Component({
  selector: 'app-employee-education-edit',
  templateUrl: './employee-education-edit.component.html',
  styleUrls: ['./employee-education-edit.component.scss']
})
export class EmployeeEducationEditComponent implements OnInit {

  educationForm = new UntypedFormGroup({
    degree: new UntypedFormControl('', [Validators.required, Validators.maxLength(200)]),
    instituteName: new UntypedFormControl('', [Validators.required, Validators.maxLength(200)]),
    passingMonth: new UntypedFormControl('', [Validators.required, Validators.min(1), Validators.max(12)]),
    passingYear: new UntypedFormControl('', [Validators.required, Validators.min(1900), Validators.max(9999)]),
    percentage: new UntypedFormControl('', [Validators.required, Validators.min(0), Validators.max(100)]),
  });

  constructor(public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private employeeEducationService: EmployeeEducationService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.getEmployeeEducation();
  }

  getEmployeeEducation() {
    this.loaderService.showLoader();
    this.employeeEducationService.getEmployeeEducationById({ id: this.config.data.educationId })
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.educationForm.get('degree')?.setValue(value.data.degree);
            this.educationForm.get('instituteName')?.setValue(value.data.instituteName);
            this.educationForm.get('passingMonth')?.setValue(value.data.passingMonth);
            this.educationForm.get('passingYear')?.setValue(value.data.passingYear);
            this.educationForm.get('percentage')?.setValue(value.data.percentage);
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

  saveEducation() {
    if (this.educationForm.invalid) {
      return;
    }
    this.loaderService.showLoader();
    var data = {
      degree: this.educationForm.get('degree')?.value,
      instituteName: this.educationForm.get('instituteName')?.value,
      passingMonth: this.educationForm.get('passingMonth')?.value,
      passingYear: this.educationForm.get('passingYear')?.value,
      percentage: this.educationForm.get('percentage')?.value,
      employeeId: this.config.data.employeeId,
      id: this.config.data.educationId
    }
    this.employeeEducationService.updateEmployeeEducation(data)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.ref.close(true);
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

  cancel() {
    this.ref.close(false);
  }

}

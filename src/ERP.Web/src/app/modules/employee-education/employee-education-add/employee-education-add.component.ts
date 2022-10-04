import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { LoaderService } from 'src/app/core/services/loader.service';
import { MessageService } from 'primeng/api';
import { EmployeeEducationService } from '../shared/employee-education.service';

@Component({
  selector: 'app-employee-education-add',
  templateUrl: './employee-education-add.component.html',
  styleUrls: ['./employee-education-add.component.scss']
})
export class EmployeeEducationAddComponent implements OnInit {

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
      employeeId: this.config.data.employeeId
    }
    this.employeeEducationService.addEmployeeEducation(data)
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

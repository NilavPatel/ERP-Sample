import { Component, Input, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { LoaderService } from 'src/app/core/services/loader.service';
import { PermissionService } from 'src/app/core/services/permission.service';
import { EmployeeEducationAddComponent } from '../employee-education-add/employee-education-add.component';
import { EmployeeEducationEditComponent } from '../employee-education-edit/employee-education-edit.component';
import { EmployeeEducationService } from '../shared/employee-education.service';

@Component({
  selector: 'app-employee-education-list',
  templateUrl: './employee-education-list.component.html',
  styleUrls: ['./employee-education-list.component.scss']
})
export class EmployeeEducationListComponent implements OnInit {

  @Input() employeeId: string | null = "";
  @Input() allowToEdit: boolean = false;

  canEdit: boolean = false;
  educations: any[] = [];

  constructor(private employeeEducationService: EmployeeEducationService,
    private loaderService: LoaderService,
    private messageService: MessageService,
    private permissionService: PermissionService,
    public dialogService: DialogService) {
    this.canEdit = this.permissionService.hasPermission(PermissionEnum.EmployeeEdit);
  }

  ngOnInit(): void {
    this.getEmployeeEducations();
  }

  getEmployeeEducations() {
    this.loaderService.showLoader();
    this.employeeEducationService
      .getEmployeeEducations({ employeeId: this.employeeId })
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.educations = value.data;
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

  showAddEducationDialog() {
    const ref = this.dialogService.open(EmployeeEducationAddComponent, {
      header: 'Add Education',
      width: '70%',
      data: {
        employeeId: this.employeeId
      }
    });
    ref.onClose.subscribe((result) => {
      if (result) {
        this.getEmployeeEducations();
      }
    });
  }

  showEditEducationDialog(id: any) {
    const ref = this.dialogService.open(EmployeeEducationEditComponent, {
      header: 'Edit Education',
      width: '70%',
      data: {
        employeeId: this.employeeId,
        educationId: id
      }
    });
    ref.onClose.subscribe((result) => {
      if (result) {
        this.getEmployeeEducations();
      }
    });
  }

  deleteEducation(id: any) {
    this.loaderService.showLoader();
    this.employeeEducationService.removeEmployeeEducation({ id: id })
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.getEmployeeEducations();
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

import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { LeaveTypeService } from 'src/app/modules/leave-types/shared/leave-type.service';

@Component({
  selector: 'app-leave-type-add',
  templateUrl: './leave-type-add.component.html',
  styleUrls: ['./leave-type-add.component.scss']
})
export class LeaveTypeAddComponent implements OnInit {

  leaveTypeName: string = "";
  description: string = "";
  isActive: boolean = false;
  countInPayroll = false;

  constructor(private router: Router,
    private leaveTypeService: LeaveTypeService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
  }

  goToList() {
    this.router.navigateByUrl("/app/leave-types/list");
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      name: this.leaveTypeName,
      description: this.description,
      isActive: this.isActive,
      countInPayroll: this.countInPayroll
    }
    this.leaveTypeService.createLeaveType(req)
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

  onClearClick(form: NgForm) {
    this.leaveTypeName = "";
    this.description = "";
    form.resetForm();
  }

}

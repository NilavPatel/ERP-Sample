import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LeaveTypeService } from 'src/app/modules/leave-types/shared/leave-type.service';
import { LoaderService } from 'src/app/core/services/loader.service';

@Component({
  selector: 'app-leave-type-edit',
  templateUrl: './leave-type-edit.component.html',
  styleUrls: ['./leave-type-edit.component.scss']
})
export class LeaveTypeEditComponent implements OnInit {

  id: string | null = "";
  leaveTypeName: string = "";
  description: string = "";
  isActive: boolean = false;
  countInPayroll = false;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private leaveTypeService: LeaveTypeService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getLeaveTypeById();
    }
  }

  getLeaveTypeById() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.leaveTypeService.getLeaveTypeById(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.leaveTypeName = value.data.name;
            this.description = value.data.description;
            this.isActive = value.data.isActive;
            this.countInPayroll = value.data.countInPayroll;
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
    this.router.navigateByUrl("/app/leave-types/list");
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      id: this.id,
      name: this.leaveTypeName,
      description: this.description,
      isActive: this.isActive,
      countInPayroll: this.countInPayroll
    }
    this.leaveTypeService.updateLeaveType(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.getLeaveTypeById();
          } else {
            this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
          }
        },
        error: (error: any) => {
          this.messageService.add({ severity: 'error', detail: error.message });
          this.loaderService.hideLoader();
        },
        complete: () => {
          this.loaderService.hideLoader();
        }
      });
  }

}

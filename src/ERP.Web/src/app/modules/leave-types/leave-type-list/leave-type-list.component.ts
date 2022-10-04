import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { LeaveTypeService } from 'src/app/modules/leave-types/shared/leave-type.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { PermissionService } from 'src/app/core/services/permission.service';

@Component({
  selector: 'app-leave-type-list',
  templateUrl: './leave-type-list.component.html',
  styleUrls: ['./leave-type-list.component.scss']
})
export class LeaveTypeListComponent implements OnInit {
  leaveTypes!: any[];
  searchKeyword: string = "";
  totalRecords!: number;
  searchTimeout: any = undefined;

  canAdd: boolean = false;
  canEdit: boolean = false;
  canDelete: boolean = false;
  
  constructor(private leaveTypeService: LeaveTypeService,
    private messageService: MessageService,
    private permissionService: PermissionService,
    private loaderService: LoaderService,
    private router: Router) { }

  ngOnInit(): void {
    this.canAdd = this.permissionService.hasPermission(PermissionEnum.LeaveTypeAdd);
    this.canEdit = this.permissionService.hasPermission(PermissionEnum.LeaveTypeEdit);
    this.canDelete = this.permissionService.hasPermission(PermissionEnum.LeaveTypeDelete);

    this.search({ page: 0, rows: 10 });
  }

  onChangeSearchKeyword() {
    if (this.searchTimeout) {
      clearTimeout(this.searchTimeout);
    }
    this.searchTimeout = setTimeout(() => {
      this.search({ page: 0, rows: 10 });
    }, 1500);
  }

  search(event: any) {
    this.loaderService.showLoader();
    var req = {
      searchKeyword: this.searchKeyword,
      pageIndex: event.page,
      pageSize: event.rows
    };
    this.leaveTypeService.getAllLeaveTypes(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.leaveTypes = value.data.result;
            this.totalRecords = value.data.count;
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

  goToAdd() {
    this.router.navigateByUrl("/app/leave-types/add");
  }

  clearSearch() {
    this.searchKeyword = "";
    this.search({ page: 0, rows: 10 });
  }

  deleteLeaveType(id: any) {
    this.loaderService.showLoader();
    var req = {
      id: id
    };
    this.leaveTypeService.deleteLeaveType(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.search({ page: 0, rows: 10 });
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

}

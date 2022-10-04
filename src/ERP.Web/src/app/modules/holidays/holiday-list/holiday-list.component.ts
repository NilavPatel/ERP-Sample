import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { HolidayService } from 'src/app/modules/holidays/shared/holiday.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { PermissionService } from 'src/app/core/services/permission.service';

@Component({
  selector: 'app-holiday-list',
  templateUrl: './holiday-list.component.html',
  styleUrls: ['./holiday-list.component.scss']
})
export class HolidayListComponent implements OnInit {

  holidays!: any[];
  searchKeyword: string = "";
  totalRecords!: number;
  searchTimeout: any = undefined;

  canAdd: boolean = false;
  canEdit: boolean = false;
  canDelete: boolean = false;

  constructor(private holidayService: HolidayService,
    private messageService: MessageService,
    private permissionService: PermissionService,
    private loaderService: LoaderService,
    private router: Router) { }

  ngOnInit(): void {
    this.canAdd = this.permissionService.hasPermission(PermissionEnum.HolidayAdd);
    this.canEdit = this.permissionService.hasPermission(PermissionEnum.HolidayEdit);
    this.canDelete = this.permissionService.hasPermission(PermissionEnum.HolidayDelete);

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
    this.holidayService.getAllHolidays(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.holidays = value.data.result;
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

  deleteHoliday(id: any) {
    this.loaderService.showLoader();
    var req = {
      id: id
    };
    this.holidayService.deleteHoliday(req).subscribe({
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

  goToAdd() {
    this.router.navigateByUrl("/app/holidays/add");
  }

  clearSearch() {
    this.searchKeyword = "";
    this.search({ page: 0, rows: 10 });
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { LoaderService } from 'src/app/core/services/loader.service';
import { PermissionService } from 'src/app/core/services/permission.service';
import { EmployeeService } from 'src/app/modules/employees/shared/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {

  employees!: any[];
  searchKeyword: string = "";
  totalRecords!: number;
  searchTimeout: any = undefined;

  canAdd: boolean = false;
  canEdit: boolean = false;

  constructor(private employeeService: EmployeeService,
    private messageService: MessageService,
    private loaderService: LoaderService,
    private permissionService: PermissionService,
    private router: Router) { }

  ngOnInit(): void {
    this.canAdd = this.permissionService.hasPermission(PermissionEnum.EmployeeAdd);
    this.canEdit = this.permissionService.hasPermission(PermissionEnum.EmployeeEdit);

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
    this.employeeService.getAllEmployees(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.employees = value.data.result;
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
    this.router.navigateByUrl("/app/employees/add");
  }

  clearSearch() {
    this.searchKeyword = "";
    this.search({ page: 0, rows: 10 });
  }
}

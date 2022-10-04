import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { EmployeeService } from 'src/app/modules/employees/shared/employee.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { RoleService } from 'src/app/modules/roles/shared/role.service';
import { UserService } from 'src/app/modules/users/shared/user.service';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.scss']
})
export class UserAddComponent implements OnInit {

  employees: any[] = [];
  roles: any[] = [];

  selectedEmployee: any;
  password: string = "";
  confirmPassword: string = "";
  selectedRoles: any[] = [];

  constructor(private employeeService: EmployeeService,
    private roleService: RoleService,
    private messageService: MessageService,
    private loaderService: LoaderService,
    private router: Router,
    private userService: UserService) { }

  ngOnInit(): void {
    this.getAllRoles();
  }

  goToList() {
    this.router.navigateByUrl("/app/users/list");
  }

  getEmployeesList(event: any) {
    var req = {
      searchKeyword: event.query,
      pageIndex: 0,
      pageSize: 50
    };
    this.employeeService.getAllEmployees(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.employees = value.data.result;
          }
        } else {
          this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
        }
      },
      error: (error: any) => {
        this.messageService.add({ severity: 'error', detail: error.message });
      }
    });
  }

  getAllRoles() {
    var req = {
      searchKeyword: '',
      pageIndex: 0,
      pageSize: 0
    };
    this.roleService.getAllRoles(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.roles = value.data.result;
          }
        } else {
          this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
        }
      },
      error: (error: any) => {
        this.messageService.add({ severity: 'error', detail: error.message });
      }
    });
  }

  onSubmit() {
    if (this.password != this.confirmPassword) {
      return;
    }
    this.loaderService.showLoader();
    var req = {
      employeeId: this.selectedEmployee.id,
      password: this.password,
      roleIds: this.selectedRoles
    }
    this.userService.registerUser(req)
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

  autoGeneratePassword() {
    this.password = this.generatePassword();
    this.confirmPassword = this.password;
  }

  generatePassword() {
    var length = 8,
      charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789#@&!",
      retVal = "";
    for (var i = 0, n = charset.length; i < length; ++i) {
      retVal += charset.charAt(Math.floor(Math.random() * n));
    }
    return retVal;
  }

  onClearClick(form: NgForm) {
    this.selectedEmployee = "";
    this.password = "";
    this.confirmPassword = "";
    this.selectedRoles = [];
    form.resetForm();
  }

}

import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { RoleService } from 'src/app/core/services/role.service';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {

  id: string | null = "";
  employeeCode: string = "";
  firstName: string = "";
  lastName: string = "";
  middleName: string = "";
  emailId: string = "";
  mobileNo: string = "";
  roleId: string = "";
  status: number | null = null;
  statusText: string = ""
  inValidLogInAttemps: number | null = null;
  lastLogInOn: any = null;

  roles: any[] = [];

  displayResetPasswordDialog: boolean = false;
  password: string = "";
  confirmPassword: string = "";

  constructor(private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private roleService: RoleService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getUserDetails();
      this.getAllRoles();
    }
  }

  goToList() {
    this.router.navigateByUrl("/app/users/list");
  }

  getUserDetails() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.userService.getUserById(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            if (value.data) {
              this.employeeCode = value.data.employeeCode;
              this.firstName = value.data.firstName;
              this.lastName = value.data.lastName;
              this.middleName = value.data.middleName;
              this.emailId = value.data.emailId;
              this.mobileNo = value.data.mobileNo;
              this.status = value.data.status;
              this.statusText = value.data.statusText;
              this.inValidLogInAttemps = value.data.inValidLogInAttemps;
              this.lastLogInOn = value.data.lastLogInOn;
              this.roleId = value.data.roleId;
            }
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

  getAllRoles() {
    var req = {
      searchKeyword: "",
      pageIndex: 0,
      pageSize: 100
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
        this.loaderService.hideLoader();
      }, complete: () => {
        this.loaderService.hideLoader();
      }
    });
  }

  blockUser() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.userService.blockUser(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.getUserDetails();
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
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

  activateUser() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.userService.activateUser(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.getUserDetails();
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
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

  showResetPasswordDialog() {
    this.displayResetPasswordDialog = true;
  }

  onSubmitResetPassword(form: NgForm) {
    if (this.password != this.confirmPassword) {
      return;
    }
    var req = {
      id: this.id,
      password: this.password
    };
    this.loaderService.showLoader();
    this.userService.resetUserPassword(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.getUserDetails();
            this.clearResetPasswordForm(form);
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

  clearResetPasswordForm(form: NgForm) {
    this.password = "";
    this.confirmPassword = "";
    form.resetForm();
    this.displayResetPasswordDialog = false;
  }

  onSubmit() {
    var req = {
      id: this.id,
      roleId: this.roleId
    };
    this.loaderService.showLoader();
    this.userService.updateUser(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.getUserDetails();
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

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.scss']
})
export class UserViewComponent implements OnInit {

  id: string | null = "";
  employeeCode: string = "";
  firstName: string = "";
  lastName: string = "";
  middleName: string = "";
  emailId: string = "";
  mobileNo: string = "";
  roleName: string = "";
  status: number | null = null;
  statusText: string = ""
  inValidLogInAttemps: number | null = null;
  lastLogInOn: any = null;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getUserDetails(this.id);
    }
  }

  goToList() {
    this.router.navigateByUrl("/app/users/list");
  }

  getUserDetails(id: any) {
    this.loaderService.showLoader();
    var req = {
      id: id
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
              this.roleName = value.data.roleName;
              this.status = value.data.status;
              this.statusText = value.data.statusText;
              this.inValidLogInAttemps = value.data.inValidLogInAttemps;
              this.lastLogInOn = value.data.lastLogInOn;
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

}

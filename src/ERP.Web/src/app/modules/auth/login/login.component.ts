import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { LoginService } from 'src/app/core/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  employeeCode: string = "";
  password: string = "";

  constructor(private loginService: LoginService,
    private authenticateServiece: AuthenticationService,
    private router: Router,
    private messageService: MessageService,
    private loaderService: LoaderService) { }

  ngOnInit(): void {
  }

  login() {
    this.loaderService.showLoader();

    var data = {
      employeeCode: this.employeeCode,
      password: this.password
    }

    this.loginService.login(data)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            if (value.data) {
              this.authenticateServiece.setCurrentUser(value.data);
              this.router.navigate(['/app/dashboard']);
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

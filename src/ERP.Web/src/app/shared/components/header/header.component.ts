import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { MenuService } from 'src/app/core/services/menu.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  fullName: string = "";

  constructor(private authenticationService: AuthenticationService,
    private router: Router,
    private menuService: MenuService) { }

  ngOnInit(): void {
    var user = this.authenticationService.getCurrentUser();
    this.fullName = user.firstName + " " + user.lastName;
  }

  logout() {
    this.authenticationService.removeCurrentUser();
    this.router.navigate(['/auth/login']);
  }

  toggleMenu() {
    this.menuService.toggleMenu();
  }
}

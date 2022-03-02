import { Component, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { MenuService } from 'src/app/core/services/menu.service';

@Component({
  selector: 'app-theme',
  templateUrl: './theme.component.html',
  styleUrls: ['./theme.component.scss']
})
export class ThemeComponent implements OnInit {

  isMenuVisible = false;

  constructor(private menuService: MenuService,
    private router: Router) { }

  ngOnInit(): void {

    this.menuService.menu.subscribe({
      next: (value) => {
        this.isMenuVisible = value
      }
    });

    this.router.events
      .subscribe({
        next: (event) => {
          if (event instanceof NavigationStart) {
            this.menuService.menu.next(false);
          }
        }
      });
  }

  onHideMenu() {
    this.menuService.menu.next(false);
  }

}

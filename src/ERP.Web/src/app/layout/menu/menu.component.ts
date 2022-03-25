import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { PermissionEnum } from 'src/app/core/enums/permission.enum';
import { PermissionService } from 'src/app/core/services/permission.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  items!: MenuItem[];

  constructor(private permissionService: PermissionService) { }

  ngOnInit(): void {
    this.items = this.getMenu();
  }

  getMenu() {
    return [{
      label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: '/app/dashboard'
    },
    {
      label: 'Employees', icon: 'pi pi-fw pi-users', routerLink: '/app/employees/list',
      visible: this.permissionService.hasPermission(PermissionEnum.EmployeeView)
    },
    {
      label: 'Users', icon: 'pi pi-fw pi-user', routerLink: '/app/users/list',
      visible: this.permissionService.hasPermission(PermissionEnum.UserView)
    },
    {
      label: 'Masters',
      icon: 'pi pi-fw pi-box',
      visible: this.isShowMasters(),
      items: [{
        label: 'Roles', icon: 'pi pi-fw pi-shield', routerLink: '/app/roles/list',
        visible: this.permissionService.hasPermission(PermissionEnum.RoleView)
      },
      {
        label: 'Designations', icon: 'pi pi-fw pi-sitemap', routerLink: '/app/designations/list',
        visible: this.permissionService.hasPermission(PermissionEnum.DesignationView)
      },
      {
        label: 'Departments', icon: 'pi pi-fw pi-th-large', routerLink: '/app/departments/list',
        visible: this.permissionService.hasPermission(PermissionEnum.DepartmentView)
      }]
    }];
  }

  isShowMasters(): boolean {
    return this.permissionService.hasAnyPermission([
      PermissionEnum.RoleView,
      PermissionEnum.DesignationView,
      PermissionEnum.DepartmentView]);
  }
}

import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { PermissionEnum } from 'src/app/shared/enums/permission.enum';
import { PermissionService } from 'src/app/core/services/permission.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  items!: MenuItem[];

  constructor(
    private permissionService: PermissionService) { }

  ngOnInit(): void {
    this.items = this.getMenu();    
  }

  getMenu() {
    return [{
      label: 'Dashboard', icon: 'fa fa-home', routerLink: '/app/dashboard'
    },
    {
      label: 'Organization',
      icon: 'fa fa-sitemap',
      visible: this.isShowOrganization(),
      items: [{
        label: 'Employees', icon: 'fa fa-users', routerLink: '/app/employees/list',
        visible: this.permissionService.hasPermission(PermissionEnum.EmployeeView)
      }, {
        label: 'Users', icon: 'fa fa-user', routerLink: '/app/users/list',
        visible: this.permissionService.hasPermission(PermissionEnum.UserView)
      }]
    },
    {
      label: 'Masters',
      icon: 'fa fa-cube',
      visible: this.isShowMasters(),
      items: [{
        label: 'Roles', icon: 'fa fa-shield', routerLink: '/app/roles/list',
        visible: this.permissionService.hasPermission(PermissionEnum.RoleView)
      }, {
        label: 'Designations', icon: 'fa fa-sitemap', routerLink: '/app/designations/list',
        visible: this.permissionService.hasPermission(PermissionEnum.DesignationView)
      }, {
        label: 'Departments', icon: 'fa fa-building', routerLink: '/app/departments/list',
        visible: this.permissionService.hasPermission(PermissionEnum.DepartmentView)
      }, {
        label: 'Leave Types', icon: 'fa fa-umbrella-beach', routerLink: '/app/leave-types/list',
        visible: this.permissionService.hasPermission(PermissionEnum.LeaveTypeView)
      }]
    }];
  }

  isShowMasters(): boolean {
    return this.permissionService.hasAnyPermission([
      PermissionEnum.RoleView,
      PermissionEnum.DesignationView,
      PermissionEnum.LeaveTypeView,
      PermissionEnum.DepartmentView]);
  }

  isShowOrganization(): boolean {
    return this.permissionService.hasAnyPermission([
      PermissionEnum.EmployeeView,
      PermissionEnum.UserView]);
  }
}

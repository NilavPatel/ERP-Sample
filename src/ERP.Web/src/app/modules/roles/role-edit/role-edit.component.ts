import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { RolePermissionService } from 'src/app/modules/roles/shared/role-permission.service';
import { RoleService } from 'src/app/modules/roles/shared/role.service';

@Component({
  selector: 'app-role-edit',
  templateUrl: './role-edit.component.html',
  styleUrls: ['./role-edit.component.scss']
})
export class RoleEditComponent implements OnInit {

  id: string | null = "";
  roleName: string = "";
  description: string = "";

  allViewChecked: boolean = false;
  allEditChecked: boolean = false;
  allAddChecked: boolean = false;
  allDeleteChecked: boolean = false;

  permissions: any[] = [];

  constructor(private router: Router,
    private route: ActivatedRoute,
    private roleService: RoleService,
    private rolePermissionService: RolePermissionService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getRoleById();
      this.getAllPermissions();
    }
  }

  getRoleById() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.roleService.getRoleById(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.roleName = value.data.name;
            this.description = value.data.description;
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

  goToList() {
    this.router.navigateByUrl("/app/roles/list");
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      id: this.id,
      name: this.roleName,
      description: this.description
    }
    this.roleService.updateRole(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.getRoleById();
          } else {
            this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
          }
        },
        error: (error: any) => {
          this.messageService.add({ severity: 'error', detail: error.message });
          this.loaderService.hideLoader();
        },
        complete: () => {
          this.loaderService.hideLoader();
        }
      });
  }

  getAllPermissions() {
    this.loaderService.showLoader();
    var req = {
      roleId: this.id
    };
    this.rolePermissionService.getAllRolePermissionByRoleId(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.permissions = this.groupByPermission(value.data);
            this.setAllcheckedValues();
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

  setAllcheckedValues() {
    this.allViewChecked = this.checkIsAllSelected(0);
    this.allEditChecked = this.checkIsAllSelected(1);
    this.allAddChecked = this.checkIsAllSelected(2);
    this.allDeleteChecked = this.checkIsAllSelected(3);
  }

  checkIsAllSelected(idx: number) {
    var isAllSelected = true;
    this.permissions.forEach(element => {
      if (idx >= element.value.length || !isAllSelected) {
        return;
      }
      isAllSelected = element.value[idx].hasPermission;
    });
    return isAllSelected;
  }

  groupByPermission(arr: any[]): any[] {
    var groupByArray = arr.reduce((group, item) => {
      const { groupName } = item;
      group[groupName] = group[groupName] ?? [];
      group[groupName].push(item);
      return group;
    }, {});

    var properties = Object.keys(groupByArray);
    var result: any[] = [];
    properties.forEach((prop: string) => {
      result.push({ key: prop, value: groupByArray[prop] });
    });
    return result;
  }

  onChangePermissions(idx: number) {
    switch (idx) {
      case 0:
        this.allViewChecked = this.checkIsAllSelected(0);
        break;
      case 1:
        this.allEditChecked = this.checkIsAllSelected(1);
        break;
      case 2:
        this.allAddChecked = this.checkIsAllSelected(2);
        break;
      case 3:
        this.allDeleteChecked = this.checkIsAllSelected(3);
        break;
    }
  }

  onSaveRolePermission() {
    this.loaderService.showLoader();
    var allowedPermissions: number[] = [];
    this.permissions.forEach(element => {
      element.value.forEach((permission: any) => {
        if (permission.hasPermission) {
          allowedPermissions.push(permission.id);
        }
      });
    });
    var req = {
      roleId: this.id,
      permissions: allowedPermissions
    };
    this.rolePermissionService.addRolePermissions(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
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

  setAllPermissions(type: string) {
    var value = false;
    var idx = 4;
    switch (type) {
      case 'view':
        value = this.allViewChecked;
        idx = 0;
        break;
      case 'edit':
        value = this.allEditChecked;
        idx = 1;
        break;
      case 'add':
        value = this.allAddChecked;
        idx = 2;
        break;
      case 'delete':
        value = this.allDeleteChecked;
        idx = 3;
        break;
    }
    this.permissions.forEach(element => {
      if (idx >= element.value.length) {
        return;
      }
      element.value[idx].hasPermission = value;
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { RolePermissionService } from 'src/app/core/services/role-permission.service';
import { RoleService } from 'src/app/core/services/role.service';

@Component({
  selector: 'app-role-view',
  templateUrl: './role-view.component.html',
  styleUrls: ['./role-view.component.scss']
})
export class RoleViewComponent implements OnInit {

  id: string | null = "";
  roleName: string = "";
  description: string = "";

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
      },
      complete: () => {
        this.loaderService.hideLoader();
      }
    });
  }

  goToList() {
    this.router.navigateByUrl("/app/role/list");
  }

  getAllPermissions() {
    var req = {
      roleId: this.id
    };
    this.rolePermissionService.getAllRolePermissionByRoleId(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.permissions = this.groupByPermission(value.data);
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

}

import { NgModule } from '@angular/core';
import { RolesRoutingModule } from './roles-routing.module';
import { RoleListComponent } from './role-list/role-list.component';
import { RoleService } from 'src/app/modules/roles/shared/role.service';
import { RoleViewComponent } from './role-view/role-view.component';
import { RoleAddComponent } from './role-add/role-add.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RolePermissionService } from './shared/role-permission.service';

@NgModule({
  declarations: [
    RoleListComponent,
    RoleViewComponent,
    RoleAddComponent,
    RoleEditComponent
  ],
  imports: [
    RolesRoutingModule,
    SharedModule
  ],
  providers: [
    RoleService,
    RolePermissionService
  ]
})
export class RolesModule { }

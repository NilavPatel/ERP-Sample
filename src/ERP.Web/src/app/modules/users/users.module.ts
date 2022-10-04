import { NgModule } from '@angular/core';
import { UsersRoutingModule } from './users-routing.module';
import { UserListComponent } from './user-list/user-list.component';
import { UserService } from 'src/app/modules/users/shared/user.service';
import { UserViewComponent } from './user-view/user-view.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserAddComponent } from './user-add/user-add.component';
import { EmployeeService } from 'src/app/modules/employees/shared/employee.service';
import { RoleService } from 'src/app/modules/roles/shared/role.service';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    UserListComponent,
    UserViewComponent,
    UserEditComponent,
    UserAddComponent
  ],
  imports: [
    UsersRoutingModule,
    SharedModule
  ],
  providers: [
    UserService,
    EmployeeService,
    RoleService
  ]
})
export class UsersModule { }

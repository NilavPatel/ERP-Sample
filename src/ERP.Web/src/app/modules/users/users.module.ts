import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { UsersRoutingModule } from './users-routing.module';
import { UserListComponent } from './user-list/user-list.component';
import { UserService } from 'src/app/core/services/user.service';
import { HttpCustomInterceptor } from 'src/app/core/interceptors/http.interceptors';

import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { TooltipModule } from 'primeng/tooltip';
import { CardModule } from 'primeng/card';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DropdownModule } from 'primeng/dropdown';

import { UserViewComponent } from './user-view/user-view.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserAddComponent } from './user-add/user-add.component';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { DialogModule } from 'primeng/dialog';
import { RoleService } from 'src/app/core/services/role.service';

@NgModule({
  declarations: [
    UserListComponent,
    UserViewComponent,
    UserEditComponent,
    UserAddComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    UsersRoutingModule,
    PaginatorModule,
    TableModule,
    TooltipModule,
    CardModule,
    ToolbarModule,
    ButtonModule,
    InputTextModule,
    AutoCompleteModule,
    DialogModule,
    DropdownModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpCustomInterceptor,
    multi: true
  }, UserService,
    EmployeeService,
    RoleService]
})
export class UsersModule { }

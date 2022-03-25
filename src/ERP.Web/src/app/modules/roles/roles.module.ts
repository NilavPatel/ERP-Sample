import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpCustomInterceptor } from 'src/app/core/interceptors/http.interceptors';

import { RolesRoutingModule } from './roles-routing.module';
import { RoleListComponent } from './role-list/role-list.component';
import { RoleService } from 'src/app/core/services/role.service';
import { RoleViewComponent } from './role-view/role-view.component';
import { RoleAddComponent } from './role-add/role-add.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { RolePermissionService } from 'src/app/core/services/role-permission.service';

import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TooltipModule } from 'primeng/tooltip';
import { CardModule } from 'primeng/card';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { TabViewModule } from 'primeng/tabview';
import { InputSwitchModule } from 'primeng/inputswitch';

@NgModule({
  declarations: [
    RoleListComponent,
    RoleViewComponent,
    RoleAddComponent,
    RoleEditComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RolesRoutingModule,
    PaginatorModule,
    TableModule,
    ToolbarModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule,
    TooltipModule,
    CardModule,
    TabViewModule,
    InputSwitchModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpCustomInterceptor,
    multi: true
  }, RoleService,
    RolePermissionService]
})
export class RolesModule { }

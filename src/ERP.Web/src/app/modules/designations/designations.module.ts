import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpCustomInterceptor } from 'src/app/core/interceptors/http.interceptors';

import { DesignationsRoutingModule } from './designations-routing.module';
import { DesignationListComponent } from './designation-list/designation-list.component';
import { DesignationService } from 'src/app/core/services/designation.service';
import { DesignationViewComponent } from './designation-view/designation-view.component';
import { DesignationAddComponent } from './designation-add/designation-add.component';
import { DesignationEditComponent } from './designation-edit/designation-edit.component';

import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TooltipModule } from 'primeng/tooltip';
import { CardModule } from 'primeng/card';
import { InputTextareaModule } from 'primeng/inputtextarea';

@NgModule({
  declarations: [
    DesignationListComponent,
    DesignationViewComponent,
    DesignationAddComponent,
    DesignationEditComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    DesignationsRoutingModule,
    PaginatorModule,
    TableModule,
    ToolbarModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule,
    TooltipModule,
    CardModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpCustomInterceptor,
    multi: true
  }, DesignationService]
})
export class DesignationsModule { }

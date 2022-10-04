import { NgModule } from '@angular/core';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { AvatarModule } from 'primeng/avatar';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { DialogModule } from 'primeng/dialog';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { DropdownModule } from 'primeng/dropdown';
import { FileUploadModule } from 'primeng/fileupload';
import { InputSwitchModule } from 'primeng/inputswitch';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MultiSelectModule } from 'primeng/multiselect';
import { PaginatorModule } from 'primeng/paginator';
import { PanelModule } from 'primeng/panel';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RadioButtonModule } from 'primeng/radiobutton';
import { SelectButtonModule } from 'primeng/selectbutton';
import { SidebarModule } from 'primeng/sidebar';
import { TabViewModule } from 'primeng/tabview';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { TooltipModule } from 'primeng/tooltip';
import { PanelMenuModule } from 'primeng/panelmenu';

@NgModule({
    declarations: [],
    imports: [
        AutoCompleteModule,
        AvatarModule,
        BlockUIModule,
        ButtonModule,
        CalendarModule,
        CardModule,
        CheckboxModule,
        DialogModule,
        DynamicDialogModule,
        DropdownModule,
        FileUploadModule,
        InputSwitchModule,
        InputTextModule,
        InputTextareaModule,
        MultiSelectModule,
        PaginatorModule,
        PanelModule,
        ProgressSpinnerModule,
        RadioButtonModule,
        SelectButtonModule,
        SidebarModule,
        TabViewModule,
        TableModule,
        ToastModule,
        ToolbarModule,
        TooltipModule,
        PanelMenuModule
    ],
    exports: [
        AutoCompleteModule,
        AvatarModule,
        BlockUIModule,
        ButtonModule,
        CalendarModule,
        CardModule,
        CheckboxModule,
        DialogModule,
        DynamicDialogModule,
        DropdownModule,
        FileUploadModule,
        InputSwitchModule,
        InputTextModule,
        InputTextareaModule,
        MultiSelectModule,
        PaginatorModule,
        PanelModule,
        ProgressSpinnerModule,
        RadioButtonModule,
        SelectButtonModule,
        SidebarModule,
        TabViewModule,
        TableModule,
        ToastModule,
        ToolbarModule,
        TooltipModule,
        PanelMenuModule
    ],
    providers: [
        DialogService // Not added in core, as when adding components in dialog service, then required http services will not get imported
    ]
})
export class PrimeNgModule { }
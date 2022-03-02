import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { PanelMenuModule } from 'primeng/panelmenu';
import { ToolbarModule } from 'primeng/toolbar';
import { SidebarModule } from 'primeng/sidebar';

import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { MenuComponent } from './menu/menu.component';
import { ThemeComponent } from './theme/theme.component';

@NgModule({
    declarations: [
        HeaderComponent,
        FooterComponent,
        MenuComponent,
        ThemeComponent
    ],
    imports: [
        CommonModule,
        ButtonModule,
        ToolbarModule,
        PanelMenuModule,
        SidebarModule        
    ],
    exports: [
        HeaderComponent,
        FooterComponent,
        MenuComponent,
        ThemeComponent
    ]
})
export class LayoutModule { }

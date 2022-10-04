import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlockableDiv } from './components/blockable-div/blockable-div.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { MenuComponent } from './components/menu/menu.component';
import { ThemeComponent } from './components/theme/theme.component';
import { PrimeNgModule } from './primeng.module';

@NgModule({
    declarations: [
        // custom defined components
        BlockableDiv,
        HeaderComponent,
        FooterComponent,
        MenuComponent,
        ThemeComponent],
    imports: [
        // angular defined modules which are going to be used in all other lazy loaded modules
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        PrimeNgModule
    ],
    exports: [
        // angular defined modules which are going to be used in all other lazy loaded modules
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        PrimeNgModule,

        // custom defined components
        BlockableDiv,
        HeaderComponent,
        FooterComponent,
        MenuComponent,
        ThemeComponent
    ]
})
export class SharedModule { }
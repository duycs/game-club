
import { CommonModule, AsyncPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';
import { InputSearchComponent } from './components/input-search/input-search.component';
import { DropdownSearchComponent } from './components/dropdown-search/dropdown-search.component';
import { MatMenuModule } from '@angular/material/menu';
import { BackButtonComponent } from './components/back-button/back-button.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
        RouterModule,
        HttpClientModule,
        MatMenuModule,
        AsyncPipe,
        RouterModule.forChild([
        ])
    ],
    declarations: [
        DropdownSearchComponent,
        InputSearchComponent,
        BackButtonComponent
    ],
    exports: [
        DropdownSearchComponent,
        InputSearchComponent,
        BackButtonComponent
    ]
})
export class SharedModule { }

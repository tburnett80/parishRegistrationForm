import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { DirectoryPageComponent } from './components/app/directoryForm/directory.page.component';

@NgModule({
    declarations: [
        AppComponent,
        DirectoryPageComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: DirectoryPageComponent },
        ])
    ]
})
export class AppModuleShared {
}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { LocalizationService } from './components/app/services/localization.service';
import { AppComponent } from './components/app/app.component';
import { DirectoryFormComponent } from './components/app/directoryForm/directory.form.component';

@NgModule({
    declarations: [
        AppComponent,
        DirectoryFormComponent
    ],
    providers: [
        LocalizationService
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'directory', pathMatch: 'full' },
            { path: 'directory', component: DirectoryFormComponent }
        ])
    ]
})
export class AppModuleShared {
}

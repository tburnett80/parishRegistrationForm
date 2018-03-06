import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { EnvironmentSettings } from './components/app/services/client.settings.service';
import { CacheService } from './components/app/services/cache.service';
import { LocalizationService } from './components/app/services/localization.service';

import { AppComponent } from './components/app/app.component';
import { CultureComponent } from './components/app/culturePicker/culture.component';
import { DirectoryFormComponent } from './components/app/directoryForm/directory.form.component';
import { DirectoryPageComponent } from './components/app/directoryForm/directory.page.component';

@NgModule({
    declarations: [
        AppComponent,
        CultureComponent,
        DirectoryFormComponent,
        DirectoryPageComponent
    ],
    providers: [
        EnvironmentSettings,
        CacheService,
        LocalizationService
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'directory', pathMatch: 'full' },
            { path: 'directory', component: DirectoryPageComponent }
        ])
    ]
})
export class AppModuleShared {
}

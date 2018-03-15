import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { TextLengthDirective } from './components/directives/text-len.directive';
import { EmailValidatorDirective } from './components/directives/email-validator.directive';

import { EnvironmentSettings } from './components/services/client.settings.service';
import { CultureChangedEmitterService } from './components/services/cultureChangedEmitter.service';
import { CacheService } from './components/services/cache.service';
import { FormConstraintsService } from './components/services/form-constraints.service';
import { LocalizationService } from './components/services/localization.service';

import { AppComponent } from './components/app.component';
import { CultureComponent } from './components/culturePicker/culture.component';
import { DirectoryFormComponent } from './components/directoryForm/directory.form.component';
import { DirectoryPageComponent } from './components/directoryForm/directory.page.component';

@NgModule({
    declarations: [
        AppComponent,
        TextLengthDirective,
        EmailValidatorDirective,
        CultureComponent,
        DirectoryFormComponent,
        DirectoryPageComponent
    ],
    providers: [
        EnvironmentSettings,
        CultureChangedEmitterService,
        CacheService,
        FormConstraintsService,
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

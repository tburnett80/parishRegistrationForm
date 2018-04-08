import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ModalDirective, ModalModule } from 'ngx-bootstrap';
import { CookiesModule } from './cookie-module/cookies.module'; 

import { TextLengthDirective } from './components/directives/text-len.directive';
import { EmailValidatorDirective } from './components/directives/email-validator.directive';
import { PhoneValidatorDirective } from './components/directives/phone-validator.directive';

import { EnvironmentSettings } from './components/services/client.settings.service';
import { CultureChangedEmitterService } from './components/services/cultureChangedEmitter.service';
import { CacheService } from './components/services/cache.service';
import { FormConstraintsService } from './components/services/form-constraints.service';
import { LocalizationService } from './components/services/localization.service';
import { DirectoryService } from './components/services/directory.service';
import { SpinnerService } from './components/services/spinner.service';

import { AppComponent } from './components/app.component';
import { CultureComponent } from './components/culturePicker/culture.component';
import { DirectoryFormComponent } from './components/directoryForm/directory.form.component';
import { DirectoryResultComponent } from './components/directoryForm/directory.result.component';
import { LoginComponent } from './components/auth/login.component';
import { UnAuthorizedComponent } from './components/auth/unauthorized.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { CommonModalComponent } from './components/modal/common-modal.component';

@NgModule({
    declarations: [
        AppComponent,
        TextLengthDirective,
        EmailValidatorDirective,
        PhoneValidatorDirective,
        CultureComponent,
        DirectoryFormComponent,
        DirectoryResultComponent,
        LoginComponent,
        UnAuthorizedComponent,
        SpinnerComponent,
        CommonModalComponent
    ],
    providers: [
        EnvironmentSettings,
        CultureChangedEmitterService,
        CacheService,
        FormConstraintsService,
        LocalizationService,
        DirectoryService,
        SpinnerService
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        CookiesModule,
        ModalModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'directory', pathMatch: 'full' },
            { path: 'directory', component: DirectoryFormComponent },
            { path: 'directory-result', component: DirectoryResultComponent },
            { path: 'logout', component: LoginComponent },
            { path: 'login', component: LoginComponent },
            { path: 'unauthorized', component: UnAuthorizedComponent },
        ])
    ]
})
export class AppModuleShared {
}

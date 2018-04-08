import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CookieService } from "./services/cookies.service";

@NgModule({
    imports: [CommonModule],
    providers: [CookieService]
})

export class CookiesModule { }
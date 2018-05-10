import { Component, OnInit, NgZone } from '@angular/core';
import { Adal4Service } from 'adal-angular4';
import { Router } from '@angular/router';

@Component({
    selector: 'welcome',
    template: `
    <div *ngIf="!IsAuthenticated" class="heading">Please login</div>
    <button *ngIf="!IsAuthenticated" class="btn btn-default" (click)="logIn()">Login</button>
    <div *ngIf="IsAuthenticated">
        <h3>already logged in....</h3>
    </div>
    `,
    styles: [`
   .heading {              
       padding-top:15px;
       padding-bottom:15px;      
    }`]
})
export class LoginComponent {

    get isAuthenticated(): boolean {
        return false;
    }

    constructor(private readonly adalService: Adal4Service,
        private readonly router: Router, private readonly zone: NgZone) {
    }

    ngOnInit() {
        this.adalService.handleWindowCallback();
        console.log(this.adalService.userInfo);
    }

    logIn() {
        this.adalService.login();
    }

    logOut() {
        this.adalService.logOut();
    }
}

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
    }

    logIn() {
        console.log('login clicked.');
        this.adalService.login();

        // Log the user information to the console
        console.log('username ' + this.adalService.userInfo.username);
        console.log('authenticated: ' + this.adalService.userInfo.authenticated);
        console.log('name: ' + this.adalService.userInfo.profile.name);
        console.log('token: ' + this.adalService.userInfo.token);
        console.log(this.adalService.userInfo.profile);
    }

    logOut() {
        console.log('logout clicked.');
        this.adalService.logOut();
    }
}

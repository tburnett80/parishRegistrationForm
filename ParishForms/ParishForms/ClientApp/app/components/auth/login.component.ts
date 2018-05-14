import { Component, OnInit, NgZone } from '@angular/core';
import { AdalService } from 'adal-angular4';
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

    constructor(private readonly adalService: AdalService,
        private readonly router: Router, private readonly zone: NgZone) {
    }

    get IsAuthenticated(): boolean {
        return this.adalService.userInfo && this.adalService.userInfo.authenticated;
    }

    ngOnInit() {
        this.adalService.handleWindowCallback();
        if (this.adalService.userInfo && this.adalService.userInfo.profile) {
            console.log(this.adalService.userInfo);
            console.log("role: ", this.adalService.userInfo.profile.roles);
        }
    }

    logIn() {
        this.adalService.login();
    }

    logOut() {
        this.adalService.logOut();
    }
}

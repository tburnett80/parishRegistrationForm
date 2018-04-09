import { Component, OnInit, NgZone } from '@angular/core';
import { AuthService } from '../services/auth.service';
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
export class LoginComponent implements OnInit {

    get isAuthenticated(): boolean {
        return this.authService.isAuthenticated;
    }

    constructor(private readonly authService: AuthService,
        private readonly router: Router, private readonly zone: NgZone) {
    }

    ngOnInit() {
        console.log('login init');
        
        //const resourceToken = localStorage.getItem('resource_token');

        //if (this.isAuthenticated && resourceToken != null) {
        //    this.router.navigateByUrl('/export');
        //} else {
        //    this.zone.run(() => {
        //        console.log('resource: ', this.AuthService.config.resource);
        //        this.adalService.acquireToken(this.adalService.config.resourceId)
        //            .subscribe((tokenOut: string) => {

        //                localStorage.setItem('id_token', tokenOut);
        //                localStorage.setItem('resource_token', 'true');

        //                window.location.href = window.location.origin + '/export';
        //            });
        //    });

        //}
    }

    logIn() {
        console.log('login clicked.');
        this.authService.login();
    }

    logOut() {
        this.authService.logOut();
    }
}

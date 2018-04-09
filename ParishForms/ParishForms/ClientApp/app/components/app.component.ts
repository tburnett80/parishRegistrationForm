import { Component } from '@angular/core';
import { Adal4Service } from 'adal-angular4';
import { AuthService } from './services/auth.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    loaderUrl: string;

    constructor(private readonly adalService: Adal4Service, private readonly authService: AuthService) {
        this.adalService.init(this.authService.adalConfig);
    }

    ngOnInit() {
        this.loaderUrl = require("./spinner/images/Squaricle-2s-100px.gif");
        //this.adalService.handleWindowCallback();
        //this.adalService.getUser();
    }
}

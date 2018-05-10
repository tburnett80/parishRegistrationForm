import { Component } from '@angular/core';
import { Adal4Service } from 'adal-angular4';

const config: any = {
    tenant: 'borromeoparish.onmicrosoft.com',
    clientId: '043e77fd-9913-4259-90ac-02ac61b90e89',
    redirectUri: 'http://localhost:3020/login'
};

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    loaderUrl: string;

    constructor(private readonly adalService: Adal4Service) {
        this.adalService.init(config);
    }

    ngOnInit() {
        this.loaderUrl = require("./spinner/images/Squaricle-2s-100px.gif");
        //this.adalService.handleWindowCallback();
        //this.adalService.getUser();
    }
}

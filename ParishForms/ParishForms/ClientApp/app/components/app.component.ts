import { Component } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    loaderUrl: string;

    ngOnInit() {
        this.loaderUrl = require("./spinner/images/Squaricle-2s-100px.gif");
    }
}

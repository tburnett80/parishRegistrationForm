import { Component } from '@angular/core';
import { LocalizationService } from '../services/localization.service';
import { EnvironmentSettings } from '../services/client.settings.service';
import { Observable, Subscription } from 'rxjs';

@Component({
    selector: 'directory-result',
    templateUrl: './directory.result.component.html',
    styleUrls: ['./directory.result.component.css']
})

export class DirectoryResultComponent {
    counter: number = 20;
    private url: string;
    private sub: Subscription;
    private timer: any;

    constructor(private service: EnvironmentSettings, private loc: LocalizationService) { }

    ngOnInit() {
        this.sub = this.service.getRedirectUrl().subscribe(data => {
            this.url = data;
            if (this.sub) 
                this.sub.unsubscribe();
        });

        setInterval(() => {
            console.log("timmer pop: ", this.counter);
            if (this.counter === 0) {
                clearInterval(this.timer);
                window.location.href = this.url;
            } else {
                this.counter--;
            }
        }, 975);
    }

    ngOnDestroy() {
        if (this.sub)
            this.sub.unsubscribe();
    }

    translate(key: string): string | undefined {
        return this.loc.translate(key);
    }
}
import { Component } from '@angular/core';
import { LocalizationService } from '../services/localization.service';
import { EnvironmentSettings } from '../services/client.settings.service';
import { CultureChangedEmitterService } from '../services/cultureChangedEmitter.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'directory-result',
    templateUrl: './directory.result.component.html',
    styleUrls: ['./directory.result.component.css']
})

export class DirectoryResultComponent {
    counter: number = 20;
    private url!: string;
    private sub!: Subscription;
    private cultureSub!: Subscription;
    private timer: any;

    constructor(private service: EnvironmentSettings, private loc: LocalizationService,
        private readonly changeEmitter: CultureChangedEmitterService) { }

    ngOnInit() {
        this.sub = this.service.getRedirectUrl().subscribe(data => {
            this.url = data;
            if (this.sub) 
                this.sub.unsubscribe();
        });

        this.cultureSub = this.changeEmitter.subscribe((next: any) => {
            this.refreshTranslations();
        });

        this.timer = setInterval(() => {
            console.log("timmer pop: ", this.counter);
            if (this.counter === 1) {
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

        if (this.cultureSub)
            this.cultureSub.unsubscribe();

        if(this.timer)
            clearInterval(this.timer);
    }

    translate(key: string): string | undefined {
        return this.loc.translate(key);
    }

    refreshTranslations() {
        console.log("refreshsing data....");
    }
}
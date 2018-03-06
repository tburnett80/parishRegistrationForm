import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalizationService } from '../services/localization.service';

@Component({
    selector: 'directory-form',
    templateUrl: './directory.form.component.html',
    styleUrls: ['./directory.form.component.css']
})

export class DirectoryFormComponent {
    private labels: any;
    private subscription: Subscription;
    private stateSub: Subscription;

    stateList: any[];
    selectedState: string;
    
    constructor(private localizationService: LocalizationService) {
    }

    ngOnInit() {
        this.subscription = this.localizationService.getFormText()
            .subscribe(data => this.labels = data);

        this.stateSub = this.localizationService.getStatesOptions()
            .subscribe(data => {
                this.stateList = data
                this.selectedState = "MO";
            });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.stateSub.unsubscribe();
    }

    translate(key: string): string | undefined {
        //Translation lookups use the english value, so we fail over to english.
        if (!this.labels || !this.labels[key]) {
            return key;
        }

        return this.labels[key];
    }

    getStatesList(): any {
        return this.stateList;
    }

    refreshTranslations() {
        console.log("refreshsing data....")
        this.subscription.unsubscribe();
        this.stateSub.unsubscribe();

        this.subscription = this.localizationService.getFormText()
            .subscribe(data => this.labels = data);

        this.stateSub = this.localizationService.getStatesOptions()
            .subscribe(data => this.stateList = data);
    }
}
//https://www.toptal.com/angular-js/angular-4-forms-validation

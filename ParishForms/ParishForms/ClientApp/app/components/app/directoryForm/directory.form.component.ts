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

    constructor(private localizationService: LocalizationService) {
    }

    ngOnInit() {
        this.subscription = this.localizationService.getFormText()
            .subscribe(data => this.labels = data);
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    getString(key: string): string | undefined {
        if (!this.labels[key]) {
            return "key not found";
        }

        return this.labels[key];
    }
}
//https://www.toptal.com/angular-js/angular-4-forms-validation

import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalizationService } from '../services/localization.service';
import { CultureChangedEmitterService } from '../services/cultureChangedEmitter.service';

@Component({
    selector: 'directory-form',
    templateUrl: './directory.form.component.html',
    styleUrls: ['./directory.form.component.css']
})

export class DirectoryFormComponent {
    private stateSub: Subscription;
    private cultureSub: Subscription;
    formModel: IDirectoryModel;

    stateList: any[];
    //selectedState: string;
    
    constructor(private localizationService: LocalizationService,
        private changeEmitter: CultureChangedEmitterService) { }

    ngOnInit() {
        this.formModel = {
            publishPhone: true,
            publisAddress: true,
            familyName: "",
            adult1FName: "",
            adult2FName: "",
            otherNames: "",
            homePhone: "",
            address: "",
            city: "",
            state: "",
            zip: "",
            adult1Email: "",
            adult2Email: "",
            adult1Cell: "",
            adult2Cell: ""
        };
        this.localizationService.initializeLocalization();
        this.stateSub = this.localizationService.getStatesOptions()
            .subscribe(data => {
                this.stateList = data;
                this.formModel.state = "MO";
            });

        this.cultureSub = this.changeEmitter.subscribe((next: any) => {
            this.refreshTranslations();
        });
    }

    ngOnDestroy() {
        this.stateSub.unsubscribe();
        this.cultureSub.unsubscribe();
    }

    translate(key: string): string | undefined {
        return this.localizationService.translate(key);
    }

    getStatesList(): any {
        return this.stateList;
    }

    refreshTranslations() {
        console.log("refreshsing data....");
    }
}
//https://www.toptal.com/angular-js/angular-4-forms-validation

/* http://localhost:3020/api/directory/limits
{
	"AdultOneFirstName": 64,
	"AdultTwoFirstName": 64,
    "FamilyName": 64,
	"OtherFamily": 1024,
	"City": 64,
	"Street": 255,
	"Zip": 10,
	"Number": 15,
	"Email": 255
}
*/
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LocalizationService } from '../services/localization.service';
import { CultureChangedEmitterService } from '../services/cultureChangedEmitter.service';
import { DirectoryService } from '../services/directory.service';

@Component({
    selector: 'directory-form',
    templateUrl: './directory.form.component.html',
    styleUrls: ['./directory.form.component.css']
})

export class DirectoryFormComponent {
    private stateSub: Subscription;
    private cultureSub: Subscription;
    private dirSub: Subscription;
    formModel: IDirectoryModel;
    stateList: any[];
    
    constructor(private localizationService: LocalizationService, private router: Router,
        private changeEmitter: CultureChangedEmitterService, private service: DirectoryService) { }

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
        if (this.dirSub)
            this.dirSub.unsubscribe();
    }

    translate(key: string): string | undefined {
        return this.localizationService.translate(key);
    }

    refreshTranslations() {
        console.log("refreshsing data....");
    }

    submit() {
        this.dirSub = this.service.storeForm(this.formModel).subscribe((data) => {
            console.log("response: ", data);
            this.router.navigate(['./directory-result']);
        });
    }
}
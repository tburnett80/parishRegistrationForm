import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription, Observable } from 'rxjs';
import { LocalizationService } from '../services/localization.service';
import { CultureChangedEmitterService } from '../services/cultureChangedEmitter.service';
import { CacheService } from '../services/cache.service';
import { DirectoryService } from '../services/directory.service';

@Component({
    selector: 'directory-form',
    templateUrl: './directory.form.component.html',
    styleUrls: ['./directory.form.component.css']
})

export class DirectoryFormComponent {
    static readonly frmKey: string = "DirectoryForm";
    private stateSub: Subscription;
    private cultureSub: Subscription;
    private dirSub: Subscription;
    formModel: IDirectoryModel;
    stateList: any[];
    
    constructor(private readonly localizationService: LocalizationService, private readonly router: Router, private readonly cache: CacheService,
        private readonly changeEmitter: CultureChangedEmitterService, private readonly service: DirectoryService) { }

    ngOnInit() {
        this.formModel = this.getFormModel();
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
        this.setFormModel();
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
        this.setFormModel();
        this.dirSub = this.service.storeForm(this.formModel)
            .subscribe(data => {
                this.router.navigate(['./directory-result']);
            }, err => {
                console.log("Error response: ", err);
            });
    }

    private getFormModel(): IDirectoryModel {
        const  partial = this.cache.getCache(DirectoryFormComponent.frmKey);
        if (partial)
            return JSON.parse(partial);

        return {
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
    }

    private setFormModel() {
        if(this.formModel)
            this.cache.setCache(DirectoryFormComponent.frmKey, JSON.stringify(this.formModel));
    }
}
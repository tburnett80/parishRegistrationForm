import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { CacheService } from './cache.service';
import { EnvironmentSettings } from './client.settings.service';
import { SpinnerService } from './spinner.service';
import { Observable, Subscription } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class FormConstraintsService {
    static calls: any = {};
    static readonly spinnerName:string = "frmConstraint";

    constructor(private readonly cache: CacheService, private http: HttpClient, private readonly spinner: SpinnerService,
        @Inject(PLATFORM_ID) private platformId: Object, private readonly settings: EnvironmentSettings) {
    }

    getLimits(formName: string): Promise<any> {
        if (!formName)
            return new Promise((resolve) => {
                resolve(undefined);
            });

        switch (formName.toLowerCase()) {
            case "directory":
                return this.getFormConstraints(formName, `/api/${formName}/limits`);
            default:
                return new Promise((resolve) => {
                    resolve(undefined);
                });
        }
    }

    private getFormConstraints(formName: string, urlPath: string): Promise<any> {
        if (isPlatformServer(this.platformId)) {
            return new Promise((resolve) => {
                 resolve(undefined);
            });
        }

        const frmKey = `${formName}::formLimits`;
        const fromCache = this.cache.getCache(frmKey);

        if (fromCache) {
            if (FormConstraintsService.calls[frmKey]) {
                FormConstraintsService.calls[frmKey] = null;
            }

            const cached = JSON.parse(fromCache);
            return new Promise((resolve) => {
                resolve(cached);
            });
        }

        if (FormConstraintsService.calls[frmKey]) {
            return FormConstraintsService.calls[frmKey];
        }

        this.spinner.show(FormConstraintsService.spinnerName);
        FormConstraintsService.calls[frmKey] = this.http.get(`${this.settings.getApiUrlBase()}${urlPath}`)
            .map((res: any) => {
                this.cache.setCache(frmKey, JSON.stringify(res));
                return res;
            })
            .do(() => this.spinner.hide(FormConstraintsService.spinnerName))
            .toPromise();

        return FormConstraintsService.calls[frmKey];
    }
}
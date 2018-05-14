import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { EnvironmentSettings } from './client.settings.service';
import { SpinnerService } from './spinner.service';
import { Observable } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class DirectoryService {
    static readonly spinnerName: string = "dirSpinner";

    constructor(private http: HttpClient, @Inject(PLATFORM_ID) private platformId: Object,
        private readonly settings: EnvironmentSettings, private readonly spinner: SpinnerService) { }

    storeForm(frm: IDirectoryModel): Observable<any> {
        if (isPlatformServer(this.platformId)) {
            return Observable.of(null);
        }

        this.spinner.show(DirectoryService.spinnerName);
        return this.http.post(`${this.settings.getApiUrlBase()}/api/directory`, frm)
            .retryWhen(err => {
                return err.flatMap((er: any) => {
                    console.log("Error returned: ", `${er.status} ( ${er.statusText} )`);
                    if (er._body)
                        console.log("returned message: ", er._body);
                    console.log("retrying form submit in 5 seconds...");
                    return Observable.of(er.status).delay(5000);
                })
                .take(3)
                .concat(Observable.throw({ error: 'Error submiting form. Retried 3 times. Please try again later.' }));
            })
            .do(() => this.spinner.hide(DirectoryService.spinnerName))
            .catch((err: any) => {
                this.spinner.hide(DirectoryService.spinnerName);
                return Observable.throw(err);
            });
    }
}

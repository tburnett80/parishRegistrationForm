import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { Http } from '@angular/http';
import { EnvironmentSettings } from './client.settings.service';
import { Observable } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class DirectoryService {
    constructor(private http: Http, @Inject(PLATFORM_ID) private platformId: Object,
        private readonly settings: EnvironmentSettings) { }

    storeForm(frm: IDirectoryModel): Observable<any> {
        if (isPlatformServer(this.platformId)) {
            return Observable.of(null);
        }

        return this.http.post(`${this.settings.getApiUrlBase()}/api/directory`, frm)
            .retryWhen(err => {
                return err.flatMap((er: any) => {
                    console.log("error tring to submit directory form: ", er.status);
                    console.log("will retry submission in 5 second.");
                    return Observable.of(er.status).delay(5000);
                })
                .take(3)
                .concat(Observable.throw({error: 'Was an error after retrying 3 times. Please try again later.'}));
            });
    }
}

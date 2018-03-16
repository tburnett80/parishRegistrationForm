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

        return this.http.post(`${this.settings.getApiUrlBase()}/api/directory`, frm);
    }
}

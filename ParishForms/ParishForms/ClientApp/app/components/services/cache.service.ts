import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';

@Injectable()
export class CacheService {
    constructor(@Inject(PLATFORM_ID) private platformId: Object) { }

    //default is 2 hours, or 7200000 milliseconds
    setCache(key: string, json: string, ttl: number = 7200000) {

        if (isPlatformServer(this.platformId)) {
            return;
        }

        if (!key) {
            console.log("cant cache when key null");
            return;
        }

        if (!json)
            return;

        const container: any = {};
        container["expires"] = new Date().getTime() + ttl;
        container["data"] = json;

        localStorage.setItem(key, JSON.stringify(container));
    }

    getCache(key: string): any | null {
        if (isPlatformServer(this.platformId)) {
            return null;
        }

        const data = localStorage.getItem(key);
        if (!data) {
            return null;
        }

        const container = JSON.parse(data);
        if (!container) {
            console.log("Could not parse json: ", data);
            return null;
        }

        if (!container.data || !container.expires) {
            console.log("Container json property null: ", data);
            return null;
        }

        if (container["expires"] < new Date().getTime()) {
            console.log("Cache expired for key: ", key);
            return null;
        }

        return container["data"];
    }
}

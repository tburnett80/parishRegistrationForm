import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';

@Injectable()
export class EnvironmentSettings {

    constructor(@Inject(PLATFORM_ID) private platformId: Object) { }

    getApiUrlBase(): string | undefined {

        if (isPlatformServer(this.platformId)) {
            return undefined;
        }

        if (!window || !window.location || !window.location.protocol && !window.location.hostname) {
            return undefined;
        }

        if (window.location.port && window.location.port !== '80' && window.location.port !== '443') {
            const port = `:${window.location.port}`;
            return `${window.location.protocol}//${window.location.hostname}${port}`;
        }

        return `${window.location.protocol}//${window.location.hostname}`;
    }
}

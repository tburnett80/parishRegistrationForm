import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { SpinnerComponent } from '../spinner/spinner.component';

@Injectable()
export class SpinnerService {
    private spinners = new Set<SpinnerComponent>();

    constructor( @Inject(PLATFORM_ID) private platformId: Object) { }

    _register(spin: SpinnerComponent) {
        if (isPlatformServer(this.platformId)) 
            return;

        this.spinners.add(spin);
    }

    show(name: string) {
        this.spinners.forEach(spin => {
            if (spin.name === name)
                spin.show = true;
        });
    }

    hide(name: string) {
        this.spinners.forEach(spin => {
            if (spin.name === name)
                spin.show = false;
        });
    }

}
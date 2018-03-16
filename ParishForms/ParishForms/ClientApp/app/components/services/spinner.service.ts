import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { SpinnerComponent } from '../spinner/spinner.component';

@Injectable()
export class SpinnerService {
    private static spinners = new Set<SpinnerComponent>();

    constructor( @Inject(PLATFORM_ID) private platformId: Object) { }

    _register(spin: SpinnerComponent) {
        if (isPlatformServer(this.platformId)) 
            return;

        SpinnerService.spinners.add(spin);
    }

    _unregister(spin: SpinnerComponent) {
        SpinnerService.spinners.forEach(sp => {
            if (sp === spin)
                SpinnerService.spinners.delete(sp);
        });
    }

    show(name: string) {
        SpinnerService.spinners.forEach(spin => {
            if (spin.name === name) {
                spin.show = true;
            }
        });
    }

    hide(name: string) {
        SpinnerService.spinners.forEach(spin => {
            if (spin.name === name)
                spin.show = false;
        });
    }

    showAll() {
        SpinnerService.spinners.forEach(s => s.show = true);
    }

    hideAll() {
        SpinnerService.spinners.forEach(s => s.show = false);
    }

    isShown(name: string) {
        let showing: boolean | undefined = undefined;
        SpinnerService.spinners.forEach(s => {
            if (s.name === name)
                showing = s.show;
        });

        return showing;
    }
}
﻿import { Component, ApplicationRef, EventEmitter, Output } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalizationService } from '../services/localization.service';
import { CultureChangedEmitterService } from '../services/cultureChangedEmitter.service';

@Component({
    selector: 'culture-selector',
    templateUrl: './culture.component.html',
    styleUrls: ['./culture.component.css']
})

export class CultureComponent {

    constructor(private readonly localizationService: LocalizationService,
        private ref: ApplicationRef, private changeEmitter: CultureChangedEmitterService) {
    }

    cultureClick(event: Event) {
        if (event.srcElement && event.srcElement.id) {
            LocalizationService.culture = event.srcElement.id;
            this.changeEmitter.next(`cultureChanged to ${LocalizationService.culture}`);
        }
    }

    getCultures() {
        return this.localizationService.getCultures();
    }

    isSelected(id: string): boolean {
        return LocalizationService.culture === id;
    }
}
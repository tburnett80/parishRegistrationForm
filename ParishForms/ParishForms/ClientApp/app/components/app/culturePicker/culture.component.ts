import { Component, ApplicationRef, EventEmitter, Output } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalizationService } from '../services/localization.service';

@Component({
    selector: 'culture-selector',
    templateUrl: './culture.component.html',
    styleUrls: ['./culture.component.css']
})

export class CultureComponent {
    @Output() cultureChanged = new EventEmitter();

    constructor(private readonly localizationService: LocalizationService, private ref: ApplicationRef) {
    }

    cultureClick(event: Event) {
        if (event.srcElement && event.srcElement.id) {
            LocalizationService.culture = event.srcElement.id;
            this.cultureChanged.emit(event.srcElement.id);
        }
    }

    isSelected(id: string): boolean {
        return LocalizationService.culture === id;
    }
}
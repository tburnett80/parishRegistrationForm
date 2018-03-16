import { Component, Input } from '@angular/core';
import { SpinnerService } from '../services/spinner.service';

//<spinner loadingImage="path/to/loading.gif" [show]="true"></spinner>
@Component({
    selector: 'spinner',
    template: `<div *ngIf="show">
                 <img *ngIf="loadingImage" [src]="loadingImage" />
                 <ng-content></ng-content>
               </div>`
})
export class SpinnerComponent {
    @Input() name: string;
    @Input() loadingImage: string;
    @Input() show = false;

    constructor(private readonly service: SpinnerService) { }

    ngOnInit() {
        if (!this.name)
            throw new Error("Spinner requires a 'name' attribute");

        this.service._register(this);
    }
}
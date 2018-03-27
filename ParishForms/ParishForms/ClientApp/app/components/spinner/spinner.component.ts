import { Component, Input, Output, EventEmitter } from '@angular/core';
import { SpinnerService } from '../services/spinner.service';
import { LocalizationService } from '../services/localization.service';

//<spinner loadingImage="path/to/loading.gif" [(show)]="true"></spinner>
@Component({
    selector: 'spinner',
    templateUrl: './spinner.component.html',
    styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent {
    private isShowing: boolean = false;

    set show(val: boolean) {
        this.isShowing = val;
        this.showChange.emit(this.isShowing);
    }

    @Input() name: string;
    @Input() loadingImage: string;
    @Input()
    get show(): boolean {
        return this.isShowing;
    }
    @Output() showChange = new EventEmitter();

    constructor(private readonly service: SpinnerService, private readonly localizationService: LocalizationService) { }

    ngOnInit() {
        if (!this.name)
            throw new Error("Spinner requires a 'name' attribute");
        this.service._register(this);
    }

    ngOnDestroy() {
        this.service._unregister(this);
    }

    translate(key: string) {
        return this.localizationService.translate(key);
    }
}
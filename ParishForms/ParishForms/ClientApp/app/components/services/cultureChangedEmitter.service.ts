import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Rx';
import 'rxjs/Rx';

@Injectable()
export class CultureChangedEmitterService {
    private events = new Subject();

    subscribe(next: any) {
        return this.events.subscribe(next);
    }

    next(event: any) {
        this.events.next(event);
    }
}
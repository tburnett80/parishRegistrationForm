import { Injectable } from '@angular/core';
import { Subject, Subscription } from 'rxjs/Rx';
import 'rxjs/Rx';
import { Subscriber } from 'rxjs/Subscriber';
import { Observable } from 'rxjs/Observable';

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
import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {CommentEvent} from "../models/comment-event";

@Injectable({
    providedIn: 'root'
})
export class CommentEventService {
    private commentEventSource = new BehaviorSubject<{event: CommentEvent | null, id: number}>(null);
    commentEvent$ = this.commentEventSource.asObservable();

    constructor() {
    }

    emitEvent(event: CommentEvent, id: number) {
        this.commentEventSource.next({event, id});
    }
}

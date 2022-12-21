import {Component, Input, OnInit} from '@angular/core';
import {Comment} from "../../models/comment";
import {CommentService} from "../../services/comment.service";
import {AuthService} from "../../services/auth.service";
import {CommentEventService} from "../../services/comment-event.service";
import {CommentEvent} from "../../models/comment-event";
import * as moment from 'moment';

@Component({
    selector: 'app-comment',
    templateUrl: './comment.component.html',
    styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnInit {
    @Input() comment: Comment
    moment = moment;

    isMarkedForDeletion: boolean = false;

    constructor(private commentService: CommentService,
                private commentEventService: CommentEventService,
                public authService: AuthService) {
    }

    ngOnInit(): void {
    }

    emitReplyEvent() {
        this.commentEventService.emitEvent(CommentEvent.Reply, this.comment.id);
    }

    emitEditEvent() {
        this.commentEventService.emitEvent(CommentEvent.Edit, this.comment.id);
    }

    markForDeletion() {
        this.commentService.markForDeletion(this.comment.id).subscribe(() => this.isMarkedForDeletion = true);
    }

    restore() {
        this.commentService.restoreComment(this.comment.id).subscribe(() => this.isMarkedForDeletion = false);
    }
}

import {Component, HostListener, Input, OnInit} from '@angular/core';
import {Comment} from "../../models/comment";
import {CommentService} from "../../services/comment.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {CommentEventService} from "../../services/comment-event.service";
import {CommentEvent} from "../../models/comment-event";
import {CommentErrorStateMatcher} from "../../extensions/comment-error-state-matcher";

@Component({
    selector: 'app-comments-list',
    templateUrl: './comments-list.component.html',
    styleUrls: ['./comments-list.component.scss']
})
export class CommentsListComponent implements OnInit {
    @Input() gameKey: string;
    comments: Comment[];

    inputHidden: boolean = true;
    isInEditMode: boolean = false;
    editCommentId: number | null = null;

    commentErrorStateMatcher = new CommentErrorStateMatcher();

    form: FormGroup = new FormGroup({
        "body": new FormControl(null,
            [Validators.required, Validators.minLength(1), Validators.maxLength(600)]),
        "parentCommentId": new FormControl(null),
    })

    @HostListener('window:beforeunload', ['$event'])
    public beforeunloadHandler() {
        this.commentService.deleteMarked(this.gameKey).subscribe();
    }

    constructor(private commentService: CommentService,
                private commentEventService: CommentEventService) {
    }

    ngOnInit(): void {
        this.commentEventService.commentEvent$.subscribe(result => {
            if (result.event === CommentEvent.Reply) {
                this.showInput();
                this.form.get("parentCommentId").setValue(result.id);
            }

            if (result.event === CommentEvent.Edit) {
                this.isInEditMode = true;
                this.editCommentId = result.id;

                let comment = this.findComment(result.id, this.comments);

                this.form.get('body').setValue(comment.body);

                this.showInput();
            }
        });

        this.commentService.getComments(this.gameKey).subscribe(result => this.comments = result);
    }

    addComment() {
        if (this.isInEditMode) {
            this.commentService.updateComment(this.editCommentId, this.form.getRawValue())
                .subscribe(() => {
                    this.closeInput();
                    this.isInEditMode = false;
                });
        }

        else {
            this.commentService.addComment(this.gameKey, this.form.getRawValue())
                .subscribe(result => {
                    this.rerenderComment(result);
                    this.closeInput()
                });
        }
    }

    private rerenderComment(comment: Comment) {
        let parentId = this.form.get("parentCommentId").value;

        if (parentId) {
            debugger;
            let parentComment = this.findComment(parentId, this.comments);
            parentComment.replies.push(comment);
        } else {
            this.comments.push(comment);
        }
    }

    showInput() {
        this.inputHidden = false;
    }

    closeInput() {
        this.form.reset();
        this.inputHidden = true;
    }

    private findComment(id: number, comments: Comment[]) {
        for (let comment of comments) {
            if(comment.id == id) {
                return comment;
            }

            let res;

            if(comment.replies) {
                for (let reply of comment.replies) {
                    res = this.findComment(id, comment.replies);
                }

                if(res) {
                    return res;
                }
            }
        }

        return null;
    }
}

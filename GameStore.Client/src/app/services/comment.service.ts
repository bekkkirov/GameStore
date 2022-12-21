import {Inject, Injectable} from '@angular/core';
import {API_BASE_URL} from "../extensions/injection-token";
import {HttpClient} from "@angular/common/http";
import {Comment} from "../models/comment";
import {CommentCreate} from "../models/comment-create";

@Injectable({
    providedIn: 'root'
})
export class CommentService {
    constructor(private http: HttpClient, @Inject(API_BASE_URL) private apiUrl: string) {
    }

    getComments(gameKey: string) {
        return this.http.get<Comment[]>(this.apiUrl + `comments/${gameKey}`);
    }

    addComment(gameKey: string, comment: CommentCreate) {
        return this.http.post<Comment>(this.apiUrl + `comments/${gameKey}/newComment`,comment);
    }

    updateComment(id: number, comment: CommentCreate) {
        return this.http.put(this.apiUrl + `comments/${id}/update`, comment);
    }

    markForDeletion(id: number) {
        return this.http.put(this.apiUrl + `comments/${id}/mark`, {});
    }

    deleteMarked(gameKey: string) {
        return this.http.delete(this.apiUrl + `comments/${gameKey}/deleteMarked`, {});
    }

    restoreComment(id: number) {
        return this.http.put(this.apiUrl + `comments/${id}/restore`, {});
    }
}

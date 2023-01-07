import {Inject, Injectable} from '@angular/core';
import {API_BASE_URL} from "../extensions/injection-token";
import {HttpClient} from "@angular/common/http";
import {User} from "../models/user";

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(@Inject(API_BASE_URL) private apiUrl: string,
                private http: HttpClient) {
    }

    getByUserName(userName: string) {
        return this.http.get<User>(this.apiUrl + `users/${userName}`);
    }

    getCurrentUser() {
        return this.http.get<User>(this.apiUrl + 'users/currentUser');
    }
}

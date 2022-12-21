import {Inject, Injectable} from '@angular/core';
import {API_BASE_URL} from "../extensions/injection-token";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {User} from "../models/user";
import {Image} from "../models/image";

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

    setProfileImage(image) {
        let headers = new HttpHeaders();
        headers.append('Content-Disposition', 'multipart/form-data');

        let formData = new FormData();
        formData.append('image', image);

        return this.http.post<Image>(this.apiUrl + 'users/image', formData, {headers});
    }
}

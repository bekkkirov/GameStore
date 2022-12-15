import {Inject, Injectable} from '@angular/core';
import {SignInData} from "../models/sign-in-data";
import {SignUpData} from "../models/sign-up-data";
import {API_BASE_URL} from "../extensions/injection-token";
import {HttpClient} from "@angular/common/http";
import {map, ReplaySubject} from "rxjs";
import {User} from "../models/user";
import {AuthResult} from "../models/auth-result";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private currentUserSource = new ReplaySubject<User | null>(1);
    public currentUser$ = this.currentUserSource.asObservable();

    constructor(@Inject(API_BASE_URL) private apiUrl: string,
                private http: HttpClient) {
    }

    setCurrentUser(user: User | null) {
        this.currentUserSource.next(user);
    }

    signIn(signInData: SignInData) {
        return this.http.post(this.apiUrl + 'auth/sign-in', signInData).pipe(
            map((response: AuthResult) => {
                this.authorize(response);
            })
        );
    }

    signUp(signUpData: SignUpData) {
        return this.http.post(this.apiUrl + 'auth/sign-up', signUpData).pipe(
            map((response: AuthResult) => {
                this.authorize(response);
            })
        );
    }

    authorize(authResult : AuthResult) {
        let user : User = {
            userName: authResult.userName,
            firstName: authResult.firstName,
            lastName: authResult.lastName,
            profileImage: authResult.profileImage
        };

        this.setCurrentUser(user);
        localStorage.setItem('authToken', authResult.token);
    }

    logout() {
        this.setCurrentUser(null);
        localStorage.removeItem('authToken');
    }
}

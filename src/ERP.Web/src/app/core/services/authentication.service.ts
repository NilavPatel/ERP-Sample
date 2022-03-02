import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    private currentUserSubject!: BehaviorSubject<any>;

    constructor() {
        this.currentUserSubject = new BehaviorSubject<any>(null);
    }

    public getCurrentUser(): any {
        var user = localStorage.getItem('currentUser');
        if (user && !this.currentUserSubject.value) {
            this.currentUserSubject.next(JSON.parse(user));
        }
        return this.currentUserSubject.value;
    }

    public removeCurrentUser(): void {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

    public setCurrentUser(user: any): void {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
    }
}
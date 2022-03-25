import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    public getCurrentUser(): any {
        var user = localStorage.getItem('currentUser');
        if (!user) {
            return null;
        }
        return JSON.parse(user);
    }

    public removeCurrentUser(): void {
        localStorage.removeItem('currentUser');
    }

    public setCurrentUser(user: any): void {
        localStorage.setItem('currentUser', JSON.stringify(user));
    }
}
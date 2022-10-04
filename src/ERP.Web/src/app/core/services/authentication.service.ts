import { Injectable } from "@angular/core";

@Injectable()
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

    public isAuhenticated(): boolean {
        const currentUser = this.getCurrentUser();
        if (currentUser) {
            return true;
        }
        return false;
    }
}
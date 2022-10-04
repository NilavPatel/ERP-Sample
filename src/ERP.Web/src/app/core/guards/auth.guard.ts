import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const isAuthenticated = this.authenticationService.isAuhenticated();
        if (isAuthenticated) {
            return true;
        }
        this.router.navigate(['/auth/login']);
        return false;
    }
}
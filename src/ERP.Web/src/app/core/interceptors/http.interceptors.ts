import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, catchError, filter, Observable, switchMap, take, throwError } from 'rxjs';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { LoginService } from 'src/app/core/services/login.service';
import { Router } from '@angular/router';
import { LoaderService } from 'src/app/core/services/loader.service';

@Injectable()
export class HttpCustomInterceptor implements HttpInterceptor {

    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(private authenticationService: AuthenticationService,
        private loginService: LoginService,
        private router: Router,
        private loaderService: LoaderService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // if below header is passed then no need to add authorization
        if (request.headers.has('x-skip-interceptor')) {
            return next.handle(request);
        }

        // add authorization token to request
        let currentUser = this.authenticationService.getCurrentUser();
        if (currentUser && currentUser.token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.token}`
                }
            });
        }

        return next.handle(request).pipe(catchError(error => {
            // if unauthorized then get new token using refresh token
            if (error instanceof HttpErrorResponse && error.status === 401) {
                return this.handle401Error(request, next);
            }
            return throwError(error);
        }));
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);

            var userData = this.authenticationService.getCurrentUser();
            return this.loginService.validateRefreshToken({ token: userData.token, refreshToken: userData.refreshToken }).pipe(switchMap((data: any) => {
                if (data && data.isValid) {
                    // set new token
                    userData.token = data.data.token;
                    userData.refreshToken = data.data.refreshToken;
                    this.authenticationService.setCurrentUser(userData);

                    this.isRefreshing = false;
                    this.refreshTokenSubject.next(userData.token);

                    return next.handle(this.addTokenHeader(request, userData.token));
                }
                return throwError(() => new Error(data.errorMessages[0]));
            }), catchError((err) => {
                this.isRefreshing = false;
                this.authenticationService.removeCurrentUser();
                this.loaderService.clearLoader();
                this.router.navigate(['/auth/login']);
                return throwError(() => err);
            }));
        }
        return this.refreshTokenSubject.pipe(
            filter(token => token !== null),
            take(1),
            switchMap((token) => next.handle(this.addTokenHeader(request, token)))
        );
    }

    private addTokenHeader(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }
}
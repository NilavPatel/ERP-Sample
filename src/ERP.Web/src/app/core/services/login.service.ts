import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from "rxjs";

@Injectable()
export class LoginService {

    constructor(private http: HttpClient) { }

    login(data: any): Observable<any> {
        var headers = { 'x-skip-interceptor': 'true' };
        return this.http.post<any>(environment.apiURL + '/User/Login', data, { headers: headers });
    }

    validateRefreshToken(data: any): Observable<any> {
        var headers = { 'x-skip-interceptor': 'true' };
        return this.http.post<any>(environment.apiURL + '/User/ValidateRefreshToken', data, { headers: headers });
    }
}
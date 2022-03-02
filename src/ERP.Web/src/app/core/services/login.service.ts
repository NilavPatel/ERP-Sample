import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from "rxjs";

@Injectable()
export class LoginService {

    constructor(private http: HttpClient) { }

    login(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/User/Login', data);
    }
}
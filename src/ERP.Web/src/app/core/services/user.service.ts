import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from '../../../environments/environment';

@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }

    getAllUsers(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/User/GetAllUsers', data);
    }

    getUserById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/User/GetUserById', data);
    }

    registerUser(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/User/RegisterUser', data);
    }

    updateUser(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/User/UpdateUser', data);
    }

    resetUserPassword(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/User/ResetUserPassword', data);
    }

    blockUser(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/User/BlockUser', data);
    }

    activateUser(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/User/ActivateUser', data);
    }
}
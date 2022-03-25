import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from '../../../environments/environment';

@Injectable()
export class RoleService {
    constructor(private http: HttpClient) { }

    getAllRoles(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Role/GetAllRoles', data);
    }

    getRoleById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Role/GetRoleById', data);
    }

    createRole(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Role/CreateRole', data);
    }

    updateRole(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Role/UpdateRole', data);
    }

    deleteRole(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Role/DeleteRole', data);
    }
}
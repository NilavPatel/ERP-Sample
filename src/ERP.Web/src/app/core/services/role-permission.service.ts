import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from '../../../environments/environment';

@Injectable()
export class RolePermissionService {
    constructor(private http: HttpClient) { }

    addRolePermissions(req: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/RolePermission/AddRolePermissions', req);
    }

    getAllRolePermissionByRoleId(req: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/RolePermission/GetAllRolePermissionByRoleId', req);
    }
}
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from '../../../environments/environment';

@Injectable()
export class DepartmentService {
    constructor(private http: HttpClient) { }

    getAllDepartments(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Department/GetAllDepartments', data);
    }

    getDepartmentById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Department/GetDepartmentById', data);
    }

    createDepartment(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Department/CreateDepartment', data);
    }

    updateDepartment(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Department/UpdateDepartment', data);
    }

    deleteDepartment(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Department/DeleteDepartment', data);
    }
}
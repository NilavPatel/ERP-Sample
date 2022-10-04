import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from 'src/environments/environment';

@Injectable()
export class EmployeeEducationService {
    constructor(private http: HttpClient) { }

    getEmployeeEducations(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeEducation/GetEmployeeEducations', data);
    }

    getEmployeeEducationById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeEducation/GetEmployeeEducationById', data);
    }

    addEmployeeEducation(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeEducation/AddEmployeeEducation', data);
    }

    updateEmployeeEducation(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeEducation/UpdateEmployeeEducation', data);
    }

    removeEmployeeEducation(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeEducation/RemoveEmployeeEducation', data);
    }
}
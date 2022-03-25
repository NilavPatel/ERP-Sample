import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from '../../../environments/environment';

@Injectable()
export class EmployeeService {
    constructor(private http: HttpClient) { }

    getAllEmployees(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/GetAllEmployees', data);
    }

    getEmployeeById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/GetEmployeeById', data);
    }

    createEmployee(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/CreateEmployee', data);
    }

    updateEmployeePersonalDetails(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/UpdateEmployeePersonalDetails', data);
    }

    updateEmployeeOfficeDetails(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/UpdateEmployeeOfficeDetails', data);
    }

    getEmployeeBankDetail(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeBankDetail/GetEmployeeBankDetail', data);
    }

    addEmployeeBankDetail(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeBankDetail/AddEmployeeBankDetail', data);
    }

    updateEmployeeBankDetail(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeBankDetail/UpdateEmployeeBankDetail', data);
    }

    uploadEmployeeDocument(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeDocument/UploadEmployeeDocument', data);
    }

    removeEmployeeDocument(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeDocument/RemoveEmployeeDocument', data);
    }

    getEmployeeDocumentsById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/EmployeeDocument/GetEmployeeDocuments', data);
    }

    downloadEmployeeDocument(id: string, token: string) {
        window.open(environment.apiURL + `/EmployeeDocument/DownloadEmployeeDocument?id=${id}&token=${token}`);
    }

    getAvailableReportingPersons(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/GetAvailableReportingPersons', data);
    }

    uploadEmployeeProfilePhoto(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/UploadEmployeeProfilePhoto', data);
    }

    removeEmployeeProfilePhoto(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/RemoveEmployeeProfilePhoto', data);
    }

    getLoginEmployeeDetails(): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Employee/GetLoginEmployeeDetails', {});
    }
}
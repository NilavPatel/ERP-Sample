import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from 'src/environments/environment';

@Injectable()
export class LeaveTypeService {
    constructor(private http: HttpClient) { }

    getAllLeaveTypes(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/LeaveType/GetAllLeaveTypes', data);
    }

    getLeaveTypeById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/LeaveType/GetLeaveTypeById', data);
    }

    createLeaveType(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/LeaveType/CreateLeaveType', data);
    }

    updateLeaveType(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/LeaveType/UpdateLeaveType', data);
    }

    deleteLeaveType(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/LeaveType/DeleteLeaveType', data);
    }

    getAllActiveLeaveTypes(): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/LeaveType/GetAllActiveLeaveTypes', {});
    }
}
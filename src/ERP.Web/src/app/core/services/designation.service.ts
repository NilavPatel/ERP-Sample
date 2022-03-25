import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from '../../../environments/environment';

@Injectable()
export class DesignationService {
    constructor(private http: HttpClient) { }

    getAllDesignations(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Designation/GetAllDesignations', data);
    }

    getDesignationById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Designation/GetDesignationById', data);
    }

    createDesignation(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Designation/CreateDesignation', data);
    }

    updateDesignation(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Designation/UpdateDesignation', data);
    }

    deleteDesignation(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Designation/DeleteDesignation', data);
    }
}
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from 'src/environments/environment';

@Injectable()
export class HolidayService {
    constructor(private http: HttpClient) { }

    getAllHolidays(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Holiday/GetAllHolidays', data);
    }

    getHolidayById(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Holiday/GetHolidayById', data);
    }

    createHoliday(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Holiday/CreateHoliday', data);
    }

    updateHoliday(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Holiday/UpdateHoliday', data);
    }

    deleteHoliday(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Holiday/DeleteHoliday', data);
    }

    getAllHolidayByYear(data: any): Observable<any> {
        return this.http.post<any>(environment.apiURL + '/Holiday/GetAllHolidayByYear', data);
    }
}
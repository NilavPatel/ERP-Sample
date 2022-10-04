import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from 'src/environments/environment';

@Injectable()
export class DashboardService {
    constructor(private http: HttpClient) { }

    getWeeklyBirthdays(): Observable<any> {
        return this.http.get<any>(environment.apiURL + '/Dashboard/GetWeeklyBirthdays');
    }
}
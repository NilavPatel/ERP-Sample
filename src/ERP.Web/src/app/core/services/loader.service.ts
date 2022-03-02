import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class LoaderService {

    loader: BehaviorSubject<boolean>;
    private count: number = 0;

    constructor() {
        this.loader = new BehaviorSubject<boolean>(false);
    }

    showLoader() {
        if (this.count == 0) {
            setTimeout(() => {
                this.loader.next(true);
            }, 0);
        }
        this.count++;
    }

    hideLoader() {
        if (this.count == 1) {
            setTimeout(() => {
                this.loader.next(false);
            }, 0);
        }
        this.count--;
    }
}
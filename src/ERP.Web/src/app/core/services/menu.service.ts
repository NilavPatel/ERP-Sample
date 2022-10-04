import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class MenuService {

    menu = new BehaviorSubject<boolean>(false);

    constructor() { }

    showMenu() {
        this.menu.next(true);
    }

    hideMenu() {
        this.menu.next(false);
    }

    toggleMenu() {
        this.menu.next(!this.menu.value);
    }
}
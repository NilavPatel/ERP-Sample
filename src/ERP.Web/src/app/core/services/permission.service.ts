import { Injectable } from "@angular/core";
import { PermissionEnum } from "../enums/permission.enum";
import { AuthenticationService } from "./authentication.service";

@Injectable({
    providedIn: 'root'
})
export class PermissionService {
    constructor(private authenticationService: AuthenticationService) {
    }

    hasPermission(permission: PermissionEnum): boolean {
        var user = this.authenticationService.getCurrentUser();
        if (user && user.permissions && user.permissions.length > 0) {
            return user.permissions.indexOf(permission) > -1;
        }
        return false;
    }
}
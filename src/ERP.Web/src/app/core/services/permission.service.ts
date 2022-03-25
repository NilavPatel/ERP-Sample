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

    hasAnyPermission(permissions: PermissionEnum[]): boolean {
        var user = this.authenticationService.getCurrentUser();
        var hasAnypermission = false;
        if (user && user.permissions && user.permissions.length > 0) {
            permissions.forEach(element => {
                if (user.permissions.indexOf(element) > -1) {
                    hasAnypermission = true;
                }
            });
        }
        return hasAnypermission;
    }
}
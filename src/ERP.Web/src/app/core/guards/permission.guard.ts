import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { PermissionEnum } from '../enums/permission.enum';
import { PermissionService } from '../services/permission.service';

@Injectable({ providedIn: 'root' })
export class PermissionGuard implements CanActivate {
    constructor(
        private router: Router,
        private permissionService: PermissionService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let permission = route.data['permission'] as PermissionEnum;
        if (permission && this.permissionService.hasPermission(permission)) {
            return true;
        }
        this.router.navigate(['/auth/login']);
        return false;
    }
}
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  /**
   * Guard route access based on the user's role
   * @returns Observable<boolean> indicating whether access is granted
   */
  canActivate(): Observable<boolean> {
    return this.authService.getRoleNameFromToken().pipe(
      map((role) => {
        if (role === 'Admin' || role === 'Manager') {
          return true;
        } else {
          this.router.navigate(['/login']);
          return false;
        }
      })
    );
  }
}

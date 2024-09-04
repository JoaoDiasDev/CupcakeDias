import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AdminGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  /**
   * If the user is not logged in or does not have the "Admin" role,
   * redirect to the login page and return false.
   * Otherwise, allow the route to load and return true.
   * @returns {boolean} true if the route is allowed, false if not
   */
  canActivate(): boolean {
    const token = this.authService.getToken();
    const userRole = this.authService.getUserRole();

    if (!token || userRole !== 'Admin') {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}

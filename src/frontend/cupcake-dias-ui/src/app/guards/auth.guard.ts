import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  /**
   * If the user has a valid JWT token, allow the route to load.
   * Otherwise, redirect to the login page.
   * @returns {boolean} true if the route is allowed, false if not
   */
  canActivate(): boolean {
    const token = this.authService.getToken(); // Get the JWT token from localStorage
    if (!token) {
      // If no token, redirect to login
      this.router.navigate(['/login']);
      return false;
    }
    return true; // Allow access if token exists
  }
}

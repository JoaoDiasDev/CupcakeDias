import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../environments/environment'; // Use this import path
import { BehaviorSubject, Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { JwtToken } from '../models/jwt-token.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'authToken';
  private userRole = new BehaviorSubject<string | null>(null);

  constructor(private http: HttpClient, private router: Router) {
    const storedToken = this.getToken();
    if (storedToken) {
      this.decodeAndSetUserRole(storedToken);
    }
  }

  /**
   * Log in the user with email and password
   * @param email User's email
   * @param password User's password
   * @returns Observable of the login request
   */
  login(email: string, password: string): Observable<any> {
    return this.http.post(`${environment.apiUrl}/users/login`, {
      email,
      password,
    });
  }

  /**
   * Store the JWT token in localStorage and decode user role
   * @param token JWT token
   */
  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    this.decodeAndSetUserRole(token);
  }

  /**
   * Retrieve the JWT token from localStorage
   * @returns JWT token or null
   */
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  /**
   * Decode the token and set the user's role
   * @param token JWT token
   */
  private decodeAndSetUserRole(token: string): void {
    try {
      const decodedToken: JwtToken = JSON.parse(atob(token.split('.')[1])); // Decode JWT
      const role = decodedToken?.role || null;
      this.userRole.next(role);
    } catch (error) {
      console.error('Error decoding token', error);
      this.userRole.next(null);
    }
  }

  /**
   * Get the current user's role
   * @returns Observable of the user's role
   */
  getUserRole(): Observable<string | null> {
    return this.userRole.asObservable();
  }

  /**
   * Check if the user is authenticated (token exists)
   * @returns True if the user is authenticated
   */
  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const decoded: JwtToken = jwtDecode<JwtToken>(token);
    const now = Math.floor(new Date().getTime() / 1000); // Current time in seconds
    if (decoded.exp < now) {
      this.logout(); // Token has expired
      return false;
    }
    return true;
  }

  /**
   * Log out the user and remove the token
   */
  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.userRole.next(null);
    this.router.navigate(['/login']);
  }
}

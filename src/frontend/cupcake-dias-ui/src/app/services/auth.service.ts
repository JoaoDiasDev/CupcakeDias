import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../environments/environment'; // Use this import path
import { BehaviorSubject, Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { JwtToken } from '../models/jwt-token.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'authToken';
  private userRole = new BehaviorSubject<string | null>(null);

  constructor(private http: HttpClient, private router: Router) {
    const token = this.getToken();
    if (token) {
      this.decodeAndSetUserRole(token);
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
  }

  /**
   * Retrieve the JWT token from localStorage
   * @returns JWT token or null
   */
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  /**
   * Return role name as an Observable
   */
  getRoleNameFromToken(): Observable<string | null> {
    const token = this.getToken();
    if (token) {
      this.decodeAndSetUserRole(token);
    }
    return this.userRole.asObservable(); // Return the user role as an observable
  }

  /**
   * Decode the token and set the role in the BehaviorSubject
   * @param token JWT token
   */
  decodeAndSetUserRole(token: string): void {
    try {
      const decodedToken: any = this.decodeToken(token);
      const role = decodedToken?.role || null;
      this.userRole.next(role);
    } catch (error) {
      console.error('Error decoding token', error);
      this.userRole.next(null);
    }
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
   * Get the logged-in user's ID from the JWT token
   * @returns The user ID or null if the token is invalid
   */
  getUserIdFromToken(): string {
    const token = this.getToken();
    if (!token) return '';

    const decodedToken = this.decodeToken(token);
    return decodedToken?.unique_name ?? '';
  }

  /**
   * Decode token
   * @returns Return valid jwt token to be manipulated
   */
  private decodeToken(token: string): JwtToken | undefined {
    try {
      return jwtDecode(token);
    } catch (error) {
      console.error('Error decoding token:', error);
      return undefined;
    }
  }

  /**
   * Log out the user and remove the token
   */
  logout(): void {
    this.userRole.next(null);
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }

  /**
   * Refresh token by making a request to the backend.
   * @param refreshToken The refresh token to be used for getting a new token.
   */
  refreshToken(refreshToken: string): Observable<string> {
    return this.http.post<string>(`${environment.apiUrl}/users/refresh-token`, {
      refreshToken,
    });
  }

  getUserById(userId: string): Observable<User> {
    return this.http.get<User>(`${environment.apiUrl}/users/${userId}`);
  }

  /**
   * Register a new user
   * @param email User's email
   * @param password User's password
   * @returns Observable of the register request
   */
  register(user: User): Observable<any> {
    return this.http.post(`${environment.apiUrl}/users`, user);
  }
}

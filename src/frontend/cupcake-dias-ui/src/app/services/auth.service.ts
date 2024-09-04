import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { jwtDecode } from 'jwt-decode';
import { JwtToken } from '../models/jwt-token.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/users`;
  private tokenKey = 'jwtToken';

  constructor(private http: HttpClient) {}

  // Login method
  login(email: string, password: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, {
      email,
      password,
    });
  }

  // Save token in localStorage
  storeToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  // Get the logged-in user's token
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  // Logout and remove token from localStorage
  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  // Decode the token and get the user role
  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }
    try {
      const decodedToken: JwtToken = jwtDecode<JwtToken>(token); // Decode the token
      return decodedToken.role; // Assuming the JWT contains the user's role
    } catch (error) {
      console.error('Error decoding token', error);
      return null;
    }
  }

  // Decode the token and get the user name
  getUserId(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }
    try {
      const decodedToken: JwtToken = jwtDecode<JwtToken>(token);
      return decodedToken.name; // Access the 'name' field (UserId)
    } catch (error) {
      console.error('Error decoding token', error);
      return null;
    }
  }
}

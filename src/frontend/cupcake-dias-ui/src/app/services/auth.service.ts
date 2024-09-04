import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/users`;

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
    localStorage.setItem('jwtToken', token);
  }

  // Get the logged-in user's token
  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  // Logout and remove token from localStorage
  logout(): void {
    localStorage.removeItem('jwtToken');
  }
}

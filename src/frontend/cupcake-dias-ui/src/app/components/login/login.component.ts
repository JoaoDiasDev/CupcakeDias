import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import {
  MatFormField,
  MatInputModule,
  MatLabel,
} from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [
    FormsModule,
    MatInputModule,
    MatButtonModule,
    CommonModule,
    MatFormField,
    MatLabel,
    RouterModule,
  ],
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  /**
   * Log in the user by sending credentials to the server
   */
  login(): void {
    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        const token = response.token;
        this.authService.decodeAndSetUserRole(token);
        this.authService.setToken(token);
        this.router.navigate(['/']); // Navigate to home or dashboard after login
      },
      error: (err) => {
        this.errorMessage = 'Invalid email or password';
        console.error('Login failed', err);
      },
    });
  }
}

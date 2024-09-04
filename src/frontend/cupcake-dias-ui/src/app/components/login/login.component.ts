import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatFormField } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [MatFormField, FormsModule],
})
export class LoginComponent {
  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        this.authService.storeToken(response.token); // Store the token
        this.router.navigate(['/']); // Navigate to the home page
      },
      error: (err) => {
        console.error('Login failed:', err);
        // Display an error message to the user
      },
    });
  }
}

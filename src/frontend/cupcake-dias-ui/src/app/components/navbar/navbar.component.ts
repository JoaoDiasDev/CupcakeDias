import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: true,
  imports: [
    RouterModule,
    MatToolbarModule, // Angular Material toolbar
    MatButtonModule, // Angular Material buttons
    MatIconModule, // Angular Material icons
    CommonModule, // To use common directives
  ],
})
export class NavbarComponent {
  constructor(public authService: AuthService, private router: Router) {}

  // Logout the user
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  // Check if user is logged in
  isLoggedIn(): boolean {
    return Boolean(this.authService.getToken());
  }
}

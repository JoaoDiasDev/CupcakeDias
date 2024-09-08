import { Component, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {
  MatButton,
  MatIconAnchor,
  MatIconButton,
} from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: true,
  imports: [
    MatIconAnchor,
    MatIconButton,
    MatButton,
    MatToolbarModule,
    MatIconModule,
    CommonModule,
    RouterModule,
  ],
})
export class NavbarComponent {
  isAdminOrManager = false;
  private roleSubscription: Subscription | undefined;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.roleSubscription = this.authService
      .getRoleNameFromToken()
      .subscribe((role) => {
        this.isAdminOrManager = role === 'Admin' || role === 'Manager';
      });
  }

  ngOnDestroy(): void {
    if (this.roleSubscription) {
      this.roleSubscription.unsubscribe();
    }
  }

  get isLoggedIn(): boolean {
    return Boolean(this.authService.getToken());
  }

  logout() {
    this.authService.logout();
  }
}

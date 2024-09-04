import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatCard, MatCardTitle } from '@angular/material/card';
import { MatLine } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import { MatFormField, MatLabel } from '@angular/material/form-field';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
  standalone: true,
  imports: [MatCard, MatCardTitle, FormsModule, MatFormField, MatLabel],
})
export class UserFormComponent implements OnInit {
  user: User = {
    userId: '',
    roleId: '',
    role: { roleId: '', roleName: '' },
    email: '',
    phoneNumber: '',
    name: '',
    passwordHash: '',
    address: '',
  };
  isNewUser = true;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isNewUser = false;
      this.loadUser(id);
    }
  }

  // Load user details for editing
  loadUser(userId: string): void {
    this.userService.getUserById(userId).subscribe({
      next: (user) => (this.user = user),
      error: (err) => console.error('Error loading user:', err),
    });
  }

  // Save user (either create or update)
  saveUser(): void {
    if (this.isNewUser) {
      this.userService.createUser(this.user).subscribe({
        next: () => {
          this.snackBar.open('User added successfully', 'Close', {
            duration: 3000,
          });
          this.router.navigate(['/users']);
        },
        error: (err) => console.error('Error adding user:', err),
      });
    } else {
      this.userService.updateUser(this.user.userId, this.user).subscribe({
        next: () => {
          this.snackBar.open('User updated successfully', 'Close', {
            duration: 3000,
          });
          this.router.navigate(['/users']);
        },
        error: (err) => console.error('Error updating user:', err),
      });
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MatCard, MatCardTitle } from '@angular/material/card';
import { MatList, MatListItem } from '@angular/material/list';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
  standalone: true,
  imports: [MatCard, MatCardTitle, MatList, MatListItem],
})
export class UserListComponent implements OnInit {
  users: User[] = [];

  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  // Fetch all users
  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (users) => (this.users = users),
      error: (err) => console.error('Error fetching users:', err),
    });
  }

  // Delete a user
  deleteUser(userId: string): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          this.snackBar.open('User deleted successfully', 'Close', {
            duration: 3000,
          });
          this.loadUsers(); // Reload users after deletion
        },
        error: (err) => console.error('Error deleting user:', err),
      });
    }
  }

  // Navigate to edit or add user
  editUser(userId: string): void {
    this.router.navigate(['/users', userId]);
  }

  // Navigate to add user
  addUser(): void {
    this.router.navigate(['/users/new']);
  }
}

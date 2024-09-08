import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { NgxMaskDirective } from 'ngx-mask';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    NgxMaskDirective,
  ],
})
export class RegisterComponent {
  registerForm: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.registerForm = this.fb.group(
      {
        email: ['', [Validators.required, Validators.email]],
        name: ['', Validators.required],
        phoneNumber: ['', [Validators.required]],
        passwordHash: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required],
        address: ['', Validators.required],
      },
      {
        validators: this.passwordMatchValidator,
      }
    );
  }

  /**
   * Custom validator to check if password and confirm password fields match
   */
  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('passwordHash')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  /**
   * Register the user by sending the form data to the server
   */
  register(): void {
    if (this.registerForm.valid) {
      const { confirmPassword, ...registerData } = this.registerForm.value;
      this.authService.register(registerData).subscribe({
        next: () => {
          this.snackBar.open('Registration successful', 'Close', {
            duration: 3000,
            verticalPosition: 'top',
            horizontalPosition: 'center',
          });
          this.router.navigate(['/login']);
        },
        error: (err) => {
          this.errorMessage = 'Registration failed';
          console.error('Registration error', err);
        },
      });
    }
  }
}

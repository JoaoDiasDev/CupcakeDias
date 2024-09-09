import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { LoadingService } from './loading.service';
import { catchError, switchMap, finalize, throwError } from 'rxjs';

// Define the interceptor function
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const loadingService = inject(LoadingService);
  const token = localStorage.getItem('authToken');

  // Start a timeout to show the loading spinner after 2 seconds
  const showSpinnerTimeout = setTimeout(() => {
    loadingService.show();
  }, 2000); // Show the spinner after 2 seconds

  if (token) {
    // Clone the request and add Authorization header
    const clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });

    return next(clonedRequest).pipe(
      catchError((error) => {
        // If the token is expired (401 Unauthorized), try to refresh the token
        if (error.status === 401) {
          const userId = authService.getUserIdFromToken();

          if (userId) {
            return authService.getUserById(userId).pipe(
              switchMap((user) => {
                if (user.refreshToken) {
                  // Call the method to refresh the token using refreshToken
                  return authService.refreshToken(user.refreshToken).pipe(
                    switchMap((newToken) => {
                      if (newToken) {
                        // Save the new token to localStorage
                        localStorage.setItem('authToken', newToken);

                        // Clone the original request with the new token
                        const newClonedRequest = req.clone({
                          setHeaders: {
                            Authorization: `Bearer ${newToken}`,
                          },
                        });

                        // Retry the original request with the new token
                        return next(newClonedRequest);
                      }

                      // If token refresh fails, log out the user or throw an error
                      return throwError(
                        () => new Error('Unable to refresh token.')
                      );
                    })
                  );
                }

                // No refresh token found, log out the user or throw an error
                return throwError(() => new Error('Refresh token not found.'));
              })
            );
          }
        }

        // If it's not a 401 error or there's no refresh token, propagate the error
        return throwError(() => error);
      }),
      // Clear the spinner timeout if the request completes before 2 seconds
      finalize(() => {
        clearTimeout(showSpinnerTimeout);
        loadingService.hide();
      })
    );
  }

  // If there's no token, proceed without modifying the request
  return next(req).pipe(
    finalize(() => {
      clearTimeout(showSpinnerTimeout);
      loadingService.hide();
    })
  );
};

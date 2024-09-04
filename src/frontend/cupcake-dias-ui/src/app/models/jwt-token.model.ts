export interface JwtToken {
  userId: string; // User Id
  role: string; // User role (e.g., 'Admin', 'Manager')
  exp: number; // Token expiration time (Unix timestamp)
}

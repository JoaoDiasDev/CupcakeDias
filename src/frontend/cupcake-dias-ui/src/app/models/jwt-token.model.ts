export interface JwtToken {
  name: string; // User ID
  role: string; // User role (e.g., 'Admin', 'Manager')
  exp: number; // Token expiration time (Unix timestamp)
}

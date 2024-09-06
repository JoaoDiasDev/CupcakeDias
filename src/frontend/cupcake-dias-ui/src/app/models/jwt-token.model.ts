export interface JwtToken {
  unique_name: string; // Maps to the user's unique identifier (userId)
  role: string; // Maps to the user's role (e.g., Admin)
  nbf: number; // "Not before" - timestamp
  exp: number; // Expiration time (Unix timestamp)
  iat: number; // Issued at time (Unix timestamp)
}

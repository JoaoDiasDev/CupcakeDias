import { Cart } from './cart.model';
import { Order } from './order.model';
import { Role } from './role.model';

export interface User {
  userId?: string; // Maps to UserId
  roleId?: string; // Maps to RoleId
  role?: Role; // Maps to Role (reference to the Role model)
  email: string; // Maps to Email
  phoneNumber: string; // Maps to PhoneNumber
  name: string; // Maps to Name
  passwordHash: string; // Maps to PasswordHash
  address: string; // Maps to Address
  refreshToken?: string; // Maps to RefreshToken
  token?: string; // JWT Token for authentication (optional)
  orders?: Order[]; // Array of Orders (optional)
  carts?: Cart[]; // Array of Carts (optional)
}

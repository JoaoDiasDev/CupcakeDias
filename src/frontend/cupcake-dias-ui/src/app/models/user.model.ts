import { Cart } from './cart.model';
import { Order } from './order.model';
import { Role } from './role.model';

export interface User {
  userId: string;
  roleId: string;
  role: Role;
  email: string;
  phoneNumber: string;
  name: string;
  passwordHash: string;
  address: string;
  token?: string; // JWT Token
  orders?: Order[];
  carts?: Cart[];
}

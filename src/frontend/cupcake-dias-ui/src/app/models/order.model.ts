import { OrderDetail } from './order-detail.model';
import { User } from './user.model';

export interface Order {
  orderId?: string; // Maps to OrderId
  userId: string; // Maps to UserId
  orderDate: Date; // Maps to OrderDate
  status: string; // Maps to Status (Pending, Processing, Completed, etc.)
  total: number; // Maps to Total
  user?: User; // Maps to related User (optional)
  orderDetails?: OrderDetail[]; // Array of OrderDetails (optional)
}

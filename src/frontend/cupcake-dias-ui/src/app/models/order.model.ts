import { OrderDetail } from "./order-detail.model";
import { User } from "./user.model";

export interface Order {
  orderId: string; // Maps to OrderId
  userId: string; // Maps to UserId
  orderDate: Date; // Maps to OrderDate
  status: string; // Maps to Status
  user?: User; // Maps to the related User
  orderDetails?: OrderDetail[]; // Maps to ICollection<OrderDetail>
}

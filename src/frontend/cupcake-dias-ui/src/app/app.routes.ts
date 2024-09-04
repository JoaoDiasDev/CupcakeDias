import { Routes } from '@angular/router';
import { CupcakeListComponent } from './components/cupcake-list/cupcake-list.component';
import { CartComponent } from './components/cart/cart.component';
import { CheckoutSuccessComponent } from './components/checkout-success/checkout-success.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import { OrderDetailComponent } from './components/order-detail/order-detail.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuardService } from './services/auth-guard.service';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'cupcakes', component: CupcakeListComponent }, // Route for cupcake list
  { path: 'cart', component: CartComponent, canActivate: [AuthGuardService] }, // Route for the cart
  {
    path: 'checkout-success',
    component: CheckoutSuccessComponent,
    canActivate: [AuthGuardService],
  }, // Route for success page
  {
    path: 'orders',
    component: OrderListComponent,
    canActivate: [AuthGuardService],
  }, // Route for viewing orders
  {
    path: 'orders/:id',
    component: OrderDetailComponent,
    canActivate: [AuthGuardService],
  }, // Route for viewing order details by ID
  { path: '', redirectTo: '/cupcakes', pathMatch: 'full' }, // Default route redirects to cupcake list
  { path: '**', redirectTo: '/cupcakes', pathMatch: 'full' }, // Catch-all for unknown routes
];

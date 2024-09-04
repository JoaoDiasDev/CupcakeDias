import { Routes } from '@angular/router';
import { CupcakeListComponent } from './components/cupcake-list/cupcake-list.component';
import { CartComponent } from './components/cart/cart.component';
import { CheckoutSuccessComponent } from './components/checkout-success/checkout-success.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import { OrderDetailComponent } from './components/order-detail/order-detail.component';
import { LoginComponent } from './components/login/login.component';
import { IngredientFormComponent } from './components/ingredient-form/ingredient-form.component';
import { IngredientListComponent } from './components/ingredient-list/ingredient-list.component';
import { AuthGuard } from './guards/auth.guard';
import { AdminGuard } from './guards/admin.guard';
import { ManagerGuard } from './guards/manager.guard';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserFormComponent } from './components/user-form/user-form.component';

export const routes: Routes = [
  {
    path: 'ingredients',
    component: IngredientListComponent,
    canActivate: [AdminGuard, ManagerGuard],
  },
  {
    path: 'ingredients/new',
    component: IngredientFormComponent,
    canActivate: [AdminGuard, ManagerGuard],
  },
  {
    path: 'ingredients/:id',
    component: IngredientFormComponent,
    canActivate: [AdminGuard, ManagerGuard],
  },
  {
    path: 'users',
    component: UserListComponent,
    canActivate: [AdminGuard], // Only accessible to Admins
  },
  {
    path: 'users/new',
    component: UserFormComponent,
    canActivate: [AdminGuard], // Only accessible to Admins
  },
  {
    path: 'users/:id',
    component: UserFormComponent,
    canActivate: [AdminGuard], // Only accessible to Admins for editing
  },
  { path: 'login', component: LoginComponent },
  { path: 'cupcakes', component: CupcakeListComponent }, // Route for cupcake list
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] }, // Route for the cart
  {
    path: 'checkout-success',
    component: CheckoutSuccessComponent,
    canActivate: [AuthGuard],
  }, // Route for success page
  {
    path: 'orders',
    component: OrderListComponent,
    canActivate: [AuthGuard],
  }, // Route for viewing orders
  {
    path: 'orders/:id',
    component: OrderDetailComponent,
    canActivate: [AuthGuard],
  }, // Route for viewing order details by ID
  { path: '', redirectTo: '/cupcakes', pathMatch: 'full' }, // Default route redirects to cupcake list
  { path: '**', redirectTo: '/cupcakes', pathMatch: 'full' }, // Catch-all for unknown routes
];

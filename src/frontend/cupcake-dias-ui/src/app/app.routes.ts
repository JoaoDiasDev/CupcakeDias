import { Routes } from '@angular/router';
import { CartComponent } from './components/cart/cart.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component'; // Import HomeComponent
import { AuthGuard } from './guards/auth.guard';
import { CupcakeListComponent } from './components/cupcake-list/cupcake-list.component';
import { RoleGuard } from './guards/role-guard.guard';
import { AdminComponent } from './components/admin/admin.component';
import { CupcakeComponent } from './components/cupcake/cupcake.component';
import { IngredientComponent } from './components/ingredient/ingredient.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'cupcakes-list',
    component: CupcakeListComponent,
    canActivate: [AuthGuard],
  },
  { path: 'admin', component: AdminComponent, canActivate: [RoleGuard] },
  {
    path: 'admin/cupcakes',
    component: CupcakeComponent,
    canActivate: [RoleGuard],
  },
  {
    path: 'admin/ingredients',
    component: IngredientComponent,
    canActivate: [RoleGuard],
  },
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home' },
];

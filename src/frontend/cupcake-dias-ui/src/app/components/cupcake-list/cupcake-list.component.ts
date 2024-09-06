import { Component, OnInit } from '@angular/core';
import { Cupcake } from '../../models/cupcake.model';
import { CupcakeService } from '../../services/cupcake.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-cupcake-list',
  templateUrl: './cupcake-list.component.html',
  styleUrls: ['./cupcake-list.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule,
    MatInputModule,
  ],
})
export class CupcakeListComponent implements OnInit {
  cupcakes: Cupcake[] = [];

  constructor(private cupcakeService: CupcakeService) {}

  ngOnInit(): void {
    this.fetchCupcakes();
  }

  fetchCupcakes(): void {
    this.cupcakeService.getCupcakes().subscribe((data: Cupcake[]) => {
      this.cupcakes = data;
    });
  }

  addToCart(cupcake: Cupcake, quantity: number): void {
    this.cupcakeService.addCupcakeToCart(cupcake, quantity).subscribe(() => {
      console.log('Cupcake added to cart');
    });
  }
}

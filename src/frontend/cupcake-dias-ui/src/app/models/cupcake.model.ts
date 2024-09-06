export interface Cupcake {
  cupcakeId: string; // Maps to CupcakeId (Guid in C# -> string in TypeScript)
  name: string; // Maps to Name
  baseFlavor: string; // Maps to BaseFlavor
  price: number; // Maps to Price (decimal in C# -> number in TypeScript)
  description: string; // Maps to Description
  imageUrl: string; // Maps to ImageUrl
  // We will ignore navigation properties here (OrderDetails, CupcakeIngredients, CartItems)
}

sequenceDiagram
    participant User
    participant CustomizationComponent
    participant CartService

    User->>CustomizationComponent: Select base flavor
    User->>CustomizationComponent: Select toppings
    User->>CustomizationComponent: Click "Add to Cart"
    CustomizationComponent->>CartService: Add item to cart
    CartService-->>CustomizationComponent: Confirmation
    CustomizationComponent-->>User: Show confirmation

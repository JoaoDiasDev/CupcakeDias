sequenceDiagram
    participant User
    participant CartComponent
    participant BackendService

    User->>CartComponent: Navigate to Cart
    CartComponent->>BackendService: Retrieve cart items
    BackendService-->>CartComponent: Return cart data
    CartComponent-->>User: Display cart items
    User->>CartComponent: Click "Proceed to Checkout"
    CartComponent->>CheckoutComponent: Navigate to Checkout

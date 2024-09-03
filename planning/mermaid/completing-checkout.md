sequenceDiagram
    participant User
    participant CheckoutComponent
    participant BackendService

    User->>CheckoutComponent: Enter email and phone
    User->>CheckoutComponent: Click "Confirm Order"
    CheckoutComponent->>BackendService: Submit order details
    BackendService-->>CheckoutComponent: Order confirmation
    CheckoutComponent-->>User: Show order confirmation

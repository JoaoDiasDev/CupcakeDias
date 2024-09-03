sequenceDiagram
    participant User
    participant OrderHistoryComponent
    participant BackendService

    User->>OrderHistoryComponent: Navigate to Order History
    OrderHistoryComponent->>BackendService: Fetch user's orders
    BackendService-->>OrderHistoryComponent: Return orders data
    OrderHistoryComponent-->>User: Display order history

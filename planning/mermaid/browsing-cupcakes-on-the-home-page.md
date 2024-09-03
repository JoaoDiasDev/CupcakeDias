sequenceDiagram
    participant User
    participant HomeComponent
    participant BackendService

    User->>HomeComponent: Navigate to Home Page
    HomeComponent->>BackendService: Fetch list of cupcakes
    BackendService-->>HomeComponent: Return cupcakes data
    HomeComponent-->>User: Display cupcakes

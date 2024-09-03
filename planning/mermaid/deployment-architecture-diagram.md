graph TD
    Client[User's Browser] -->|HTTP Requests| Frontend[Angular Frontend]
    Frontend -->|API Calls| Backend[Backend API Server]
    Backend -->|Database Queries| Database[(Database)]

classDiagram
    class AppComponent {
        +router-outlet
    }
    class HomeComponent {
        +cupcakes: string[]
        +ngOnInit()
    }
    class CustomizationComponent {
        +selectedBase: string
        +selectedToppings: string[]
        +addTopping(topping: string)
        +ngOnInit()
    }
    class CartComponent {
        +items: CartItem[]
        +getTotal(): number
        +ngOnInit()
    }
    class CheckoutComponent {
        +email: string
        +phoneNumber: string
        +isValidEmail(): boolean
        +isValidPhone(): boolean
        +ngOnInit()
    }
    class OrderHistoryComponent {
        +orders: Order[]
        +ngOnInit()
    }
    class CartService {
        +addItem(item: CartItem)
        +getCartItems(): CartItem[]
    }
    class BackendService {
        +fetchCupcakes(): string[]
        +submitOrder(orderDetails: Order): any
        +fetchOrders(userId: string): Order[]
    }

    class CartItem {
        +name: string
        +quantity: number
        +price: number
    }

    class Order {
        +id: number
        +date: string
        +items: CartItem[]
    }

    AppComponent --> RouterOutlet
    RouterOutlet --> HomeComponent
    RouterOutlet --> CustomizationComponent
    RouterOutlet --> CartComponent
    RouterOutlet --> CheckoutComponent
    RouterOutlet --> OrderHistoryComponent

    HomeComponent --> BackendService : Uses
    CustomizationComponent --> CartService : Uses
    CartComponent --> CartItem : Contains
    CartComponent --> BackendService : Uses
    CheckoutComponent --> BackendService : Uses
    OrderHistoryComponent --> BackendService : Uses
    OrderHistoryComponent --> Order : Contains
classDiagram
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
    class BackendService {
        +fetchCupcakes(): any
        +submitOrder(orderDetails: any): any
        +fetchOrders(userId: any): any
    }
    class CartService {
        +addItem(item: CartItem)
        +getCartItems(): CartItem[]
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

    HomeComponent --> BackendService : fetchCupcakes()
    CustomizationComponent --> CartService : addItem()
    CartComponent --> CartItem : Contains
    CartComponent --> BackendService : retrieveCartItems()
    CheckoutComponent --> BackendService : submitOrder()
    OrderHistoryComponent --> BackendService : fetchOrders()
    OrderHistoryComponent --> Order : Contains

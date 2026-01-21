using ECommerceFacadeDemo.Facades;
using ECommerceFacadeDemo.Subsystems;

// Initialize subsystem services
var inventoryService = new InventoryService();
var paymentService = new PaymentService();
var shippingService = new ShippingService();

// Create facade with dependencies
var orderFacade = new OrderFacade(inventoryService, paymentService, shippingService);

Console.WriteLine("🛍️  E-Commerce Facade Pattern Demo");
Console.WriteLine("==================================\n");

// Test Case 1: Successful order
orderFacade.PlaceOrder(
    orderId: "ORD001",
    productId: "PROD001",
    quantity: 5,
    amount: 99.99m,
    address: "123 Main St, New York, NY"
);

// Test Case 2: Out of stock
orderFacade.PlaceOrder(
    orderId: "ORD002",
    productId: "PROD003",
    quantity: 1,
    amount: 50.00m,
    address: "456 Oak Ave, Los Angeles, CA"
);

// Test Case 3: Payment failure
orderFacade.PlaceOrder(
    orderId: "FAIL_ORD003",
    productId: "PROD002",
    quantity: 3,
    amount: 75.50m,
    address: "789 Pine Rd, Chicago, IL"
);

// Test Case 4: Another successful order
orderFacade.PlaceOrder(
    orderId: "ORD004",
    productId: "PROD002",
    quantity: 2,
    amount: 50.00m,
    address: "321 Elm Blvd, Houston, TX"
);

Console.WriteLine("\n✅ Demo completed!");

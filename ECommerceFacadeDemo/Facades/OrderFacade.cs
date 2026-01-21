using ECommerceFacadeDemo.Subsystems;

namespace ECommerceFacadeDemo.Facades
{
    /// <summary>
    /// Facade that simplifies the complex order processing workflow
    /// by coordinating inventory, payment, and shipping subsystems
    /// </summary>
    public class OrderFacade
    {
        private readonly IInventoryService _inventoryService;
        private readonly IPaymentService _paymentService;
        private readonly IShippingService _shippingService;

        public OrderFacade(
            IInventoryService inventoryService,
            IPaymentService paymentService,
            IShippingService shippingService)
        {
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));
        }

        /// <summary>
        /// Processes a complete order in a single method call
        /// Hides the complexity of coordinating multiple subsystems
        /// </summary>
        public bool PlaceOrder(string orderId, string productId, int quantity, decimal amount, string address)
        {
            Console.WriteLine($"\n📋 Processing Order: {orderId}");
            Console.WriteLine(new string('-', 50));

            // Step 1: Check inventory
            if (!_inventoryService.CheckStock(productId, quantity))
            {
                Console.WriteLine($"❌ Order {orderId} FAILED: Product out of stock");
                return false;
            }

            // Step 2: Process payment
            if (!_paymentService.ProcessPayment(orderId, amount))
            {
                Console.WriteLine($"❌ Order {orderId} FAILED: Payment processing failed");
                return false;
            }

            // Step 3: Reserve stock
            _inventoryService.ReserveStock(productId, quantity);

            // Step 4: Arrange shipment
            _shippingService.ArrangeShipment(orderId, address);

            Console.WriteLine($"✅ Order {orderId} COMPLETED successfully!");
            Console.WriteLine(new string('-', 50));

            return true;
        }
    }
}

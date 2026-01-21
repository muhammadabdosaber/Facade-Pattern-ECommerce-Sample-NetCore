namespace ECommerceFacadeDemo.Subsystems
{
    /// <summary>
    /// Handles shipping and order delivery
    /// </summary>
    public interface IShippingService
    {
        void ArrangeShipment(string orderId, string address);
    }

    public class ShippingService : IShippingService
    {
        public void ArrangeShipment(string orderId, string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                Console.WriteLine("❌ Shipping: Invalid address provided");
                return;
            }

            Console.WriteLine($"✓ Shipping: Shipment arranged for order {orderId} to {address}");
        }
    }
}

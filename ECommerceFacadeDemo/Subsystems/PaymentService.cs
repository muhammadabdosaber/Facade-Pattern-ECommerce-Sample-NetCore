namespace ECommerceFacadeDemo.Subsystems
{
    /// <summary>
    /// Processes payments for orders
    /// </summary>
    public interface IPaymentService
    {
        bool ProcessPayment(string orderId, decimal amount);
    }

    public class PaymentService : IPaymentService
    {
        public bool ProcessPayment(string orderId, decimal amount)
        {
            // Simulate payment processing
            if (amount <= 0)
            {
                Console.WriteLine("❌ Payment: Invalid amount");
                return false;
            }

            if (orderId.StartsWith("FAIL"))
            {
                Console.WriteLine($"❌ Payment: Failed to process payment of ${amount} for order {orderId}");
                return false;
            }

            Console.WriteLine($"✓ Payment: Successfully processed ${amount} for order {orderId}");
            return true;
        }
    }
}

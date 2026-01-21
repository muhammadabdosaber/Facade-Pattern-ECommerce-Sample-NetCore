namespace ECommerceFacadeDemo.Subsystems
{
    /// <summary>
    /// Manages product inventory and stock availability
    /// </summary>
    public interface IInventoryService
    {
        bool CheckStock(string productId, int quantity);
        void ReserveStock(string productId, int quantity);
    }

    public class InventoryService : IInventoryService
    {
        private readonly Dictionary<string, int> _inventory = new()
        {
            { "PROD001", 100 },
            { "PROD002", 50 },
            { "PROD003", 0 }
        };

        public bool CheckStock(string productId, int quantity)
        {
            if (!_inventory.ContainsKey(productId))
            {
                Console.WriteLine($"❌ Inventory: Product {productId} not found");
                return false;
            }

            bool isAvailable = _inventory[productId] >= quantity;
            if (isAvailable)
            {
                Console.WriteLine($"✓ Inventory: {quantity} units of {productId} available");
            }
            else
            {
                Console.WriteLine($"❌ Inventory: Insufficient stock for {productId}. Available: {_inventory[productId]}, Requested: {quantity}");
            }

            return isAvailable;
        }

        public void ReserveStock(string productId, int quantity)
        {
            if (_inventory.ContainsKey(productId) && _inventory[productId] >= quantity)
            {
                _inventory[productId] -= quantity;
                Console.WriteLine($"✓ Inventory: Reserved {quantity} units of {productId}");
            }
        }
    }
}

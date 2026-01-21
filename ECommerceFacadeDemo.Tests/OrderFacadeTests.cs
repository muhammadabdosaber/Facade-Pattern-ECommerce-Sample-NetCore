using ECommerceFacadeDemo.Facades;
using ECommerceFacadeDemo.Subsystems;
using Moq;

namespace ECommerceFacadeDemo.Tests;

public class OrderFacadeTests
{
    [Fact]
    public void PlaceOrder_WithValidOrder_ReturnsTrue()
    {
        // Arrange
        var mockInventory = new Mock<IInventoryService>();
        var mockPayment = new Mock<IPaymentService>();
        var mockShipping = new Mock<IShippingService>();

        mockInventory.Setup(x => x.CheckStock(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
        mockPayment.Setup(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<decimal>())).Returns(true);

        var facade = new OrderFacade(
            mockInventory.Object,
            mockPayment.Object,
            mockShipping.Object
        );

        // Act
        var result = facade.PlaceOrder("ORD001", "PROD001", 5, 99.99m, "123 Main St");

        // Assert
        Assert.True(result);
        mockInventory.Verify(x => x.CheckStock("PROD001", 5), Times.Once);
        mockPayment.Verify(x => x.ProcessPayment("ORD001", 99.99m), Times.Once);
        mockInventory.Verify(x => x.ReserveStock("PROD001", 5), Times.Once);
        mockShipping.Verify(x => x.ArrangeShipment("ORD001", "123 Main St"), Times.Once);
    }

    [Fact]
    public void PlaceOrder_WithOutOfStockProduct_ReturnsFalse()
    {
        // Arrange
        var mockInventory = new Mock<IInventoryService>();
        var mockPayment = new Mock<IPaymentService>();
        var mockShipping = new Mock<IShippingService>();

        mockInventory.Setup(x => x.CheckStock(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

        var facade = new OrderFacade(
            mockInventory.Object,
            mockPayment.Object,
            mockShipping.Object
        );

        // Act
        var result = facade.PlaceOrder("ORD002", "PROD001", 5, 99.99m, "123 Main St");

        // Assert
        Assert.False(result);
        mockPayment.Verify(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<decimal>()), Times.Never);
        mockShipping.Verify(x => x.ArrangeShipment(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void PlaceOrder_WithPaymentFailure_ReturnsFalse()
    {
        // Arrange
        var mockInventory = new Mock<IInventoryService>();
        var mockPayment = new Mock<IPaymentService>();
        var mockShipping = new Mock<IShippingService>();

        mockInventory.Setup(x => x.CheckStock(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
        mockPayment.Setup(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<decimal>())).Returns(false);

        var facade = new OrderFacade(
            mockInventory.Object,
            mockPayment.Object,
            mockShipping.Object
        );

        // Act
        var result = facade.PlaceOrder("ORD003", "PROD001", 5, 99.99m, "123 Main St");

        // Assert
        Assert.False(result);
        mockShipping.Verify(x => x.ArrangeShipment(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        mockInventory.Verify(x => x.ReserveStock(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void PlaceOrder_OrderProcessingSequence_IsCorrect()
    {
        // Arrange
        var callSequence = new List<string>();

        var mockInventory = new Mock<IInventoryService>();
        var mockPayment = new Mock<IPaymentService>();
        var mockShipping = new Mock<IShippingService>();

        mockInventory.Setup(x => x.CheckStock(It.IsAny<string>(), It.IsAny<int>()))
            .Callback(() => callSequence.Add("CheckStock"))
            .Returns(true);

        mockPayment.Setup(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<decimal>()))
            .Callback(() => callSequence.Add("ProcessPayment"))
            .Returns(true);

        mockInventory.Setup(x => x.ReserveStock(It.IsAny<string>(), It.IsAny<int>()))
            .Callback(() => callSequence.Add("ReserveStock"));

        mockShipping.Setup(x => x.ArrangeShipment(It.IsAny<string>(), It.IsAny<string>()))
            .Callback(() => callSequence.Add("ArrangeShipment"));

        var facade = new OrderFacade(
            mockInventory.Object,
            mockPayment.Object,
            mockShipping.Object
        );

        // Act
        facade.PlaceOrder("ORD001", "PROD001", 5, 99.99m, "123 Main St");

        // Assert
        Assert.Equal(new[] { "CheckStock", "ProcessPayment", "ReserveStock", "ArrangeShipment" }, callSequence);
    }

    [Fact]
    public void PlaceOrder_NullInventoryService_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new OrderFacade(null, new Mock<IPaymentService>().Object, new Mock<IShippingService>().Object)
        );
    }
}
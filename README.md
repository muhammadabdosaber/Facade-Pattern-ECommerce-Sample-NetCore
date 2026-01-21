# Facade Design Pattern – E-Commerce (.NET Core)

## Purpose

This repository demonstrates the **Facade Design Pattern** using a simple **E-Commerce order processing workflow** implemented in **.NET Core**. It is designed to be **GitHub Copilot friendly**, easy to understand, and easy to extend.

---

## 🏗 Architecture Overview

The system simulates placing an order in an e-commerce application by coordinating:

- **Inventory Service** - Manages product stock
- **Payment Service** - Processes customer payments
- **Shipping Service** - Arranges shipment and delivery

The **OrderFacade** provides a single entry point that hides the complexity of these subsystems.

```
Client → OrderFacade → (Inventory | Payment | Shipping)
```

---

## 📁 Project Structure

```
ECommerceFacadeDemo/
│
├── Facades/
│   └── OrderFacade.cs
│
├── Subsystems/
│   ├── InventoryService.cs
│   ├── PaymentService.cs
│   └── ShippingService.cs
│
├── Program.cs
│
└── ECommerceFacadeDemo.Tests/
    └── OrderFacadeTests.cs
```

---

## ✅ Key Design Pattern Concepts

- **Facade** simplifies complex workflows
- Client is decoupled from subsystem logic
- Easier testing and maintenance
- Single Responsibility Principle

---

## 🧪 Unit Testing

The test project uses:

- **xUnit** for test framework
- **Moq** for mocking subsystem dependencies

### What Is Tested

✅ Successful order placement  
✅ Inventory out-of-stock scenario  
✅ Payment failure scenario  
✅ Order processing sequence correctness  
✅ Null dependency handling  
✅ No direct dependency on concrete implementations  

---

## ▶ Running the Application

```bash
dotnet run --project ECommerceFacadeDemo/ECommerceFacadeDemo.csproj
```

### Expected Output

```
🛍️  E-Commerce Facade Pattern Demo
==================================

📋 Processing Order: ORD001
--------------------------------------------------
✓ Inventory: 5 units of PROD001 available
✓ Payment: Successfully processed $99.99 for order ORD001
✓ Inventory: Reserved 5 units of PROD001
✓ Shipping: Shipment arranged for order ORD001 to 123 Main St, New York, NY
✅ Order ORD001 COMPLETED successfully!
--------------------------------------------------

✅ Demo completed!
```

---

## 🧪 Running Tests

```bash
dotnet test ECommerceFacadeDemo.sln
```

---

## 🤖 GitHub Copilot Usage Tips

This repository is optimized for Copilot:

- Clear naming conventions
- One class per responsibility
- Predictable folder structure
- Well-documented interfaces
- Type-safe implementations

---

## 📌 License

MIT License – educational use

---

## 🚀 Getting Started

1. Clone the repository:
```bash
git clone https://github.com/muhammadabdosaber/Facade-Pattern-ECommerce-Sample-NetCore.git
cd Facade-Pattern-ECommerce-Sample-NetCore
```

2. Build the solution:
```bash
dotnet build
```

3. Run the demo:
```bash
dotnet run --project ECommerceFacadeDemo/ECommerceFacadeDemo.csproj
```

4. Run the tests:
```bash
dotnet test
```

---

## 📚 Pattern Explanation

### What is the Facade Pattern?

The Facade Pattern is a structural design pattern that provides a unified, simplified interface to a set of interfaces in a subsystem. It hides the complexity of the system and provides a simple interface to clients.

### When to Use

- When you need to provide a simple interface to a complex subsystem
- When the subsystem is complex and has many interdependencies
- When you want to decouple client code from subsystem components

### Benefits

- Simplifies the client code
- Reduces coupling between client and subsystems
- Makes the system easier to test and maintain
- Single entry point for related operations

---


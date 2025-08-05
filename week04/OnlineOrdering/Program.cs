using System;
using System.Collections.Generic;
using System.Text;


// Addresss Class

public class Addresss
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Addresss(string street, string city, string stateOrProvince, string country)
    {
        _street = street ?? string.Empty;
        _city = city ?? string.Empty;
        _stateOrProvince = stateOrProvince ?? string.Empty;
        _country = country ?? string.Empty;
    }

    public bool IsInUSA()
    {
        var c = _country?.Trim().ToLowerInvariant() ?? string.Empty;
        return c == "usa" || c == "united states" || c == "united states of america" || c == "us";
    }

    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}

// Customer Class
public class Customer
{
    private string _name;
    private Addresss _address;

    public Customer(string name, Addresss address)
    {
        _name = name ?? string.Empty;
        _address = address ?? throw new ArgumentNullException(nameof(address));
    }

    public string GetName() => _name;

    public Addresss GetAddress() => _address;

    public bool LivesInUSA() => _address.IsInUSA();
}

// Product Classss
public class Product
{
    private string _name;
    private string _productId;
    private decimal _pricePerUnit;
    private int _quantity;

    public Product(string name, string productId, decimal pricePerUnit, int quantity)
    {
        _name = name ?? string.Empty;
        _productId = productId ?? string.Empty;
        _pricePerUnit = pricePerUnit >= 0 ? pricePerUnit : 0m;
        _quantity = Math.Max(0, quantity);
    }

    public string GetName() => _name;
    public string GetProductId() => _productId;
    public decimal GetPricePerUnit() => _pricePerUnit;
    public int GetQuantity() => _quantity;

    public decimal GetTotalCost()
    {
        return _pricePerUnit * _quantity;
    }
}

// ==========================
// Order Class
// ==========================
public class Order
{
    private List<Product> _products;
    private Customer _customer;

    private const decimal DomesticShipping = 5.00m;
    private const decimal InternationalShipping = 35.00m;

    public Order(Customer customer)
    {
        _customer = customer ?? throw new ArgumentNullException(nameof(customer));
        _products = new List<Product>();
    }

    public void AddProduct(Product p)
    {
        if (p != null)
            _products.Add(p);
    }

    public decimal GetTotalPrice()
    {
        decimal sum = 0m;
        foreach (var p in _products)
        {
            sum += p.GetTotalCost();
        }

        decimal shipping = _customer.LivesInUSA() ? DomesticShipping : InternationalShipping;
        return sum + shipping;
    }

    public string GetPackingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Packing Label:");
        foreach (var p in _products)
        {
            sb.AppendLine($"{p.GetName()} - {p.GetProductId()}");
        }
        return sb.ToString().TrimEnd();
    }

    public string GetShippingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Shipping Label:");
        sb.AppendLine(_customer.GetName());
        sb.AppendLine(_customer.GetAddress().GetFullAddress());
        return sb.ToString().TrimEnd();
    }
}

// ==========================
// Program Entry Point
// ==========================
class Program
{
    static void Main(string[] args)
    {
        // Order 1 (Domestic)
        var addr1 = new Addresss("123 Main St", "Springfield", "IL", "USA");
        var cust1 = new Customer("John Doe", addr1);
        var order1 = new Order(cust1);
        order1.AddProduct(new Product("Widget A", "WID-001", 9.99m, 2));
        order1.AddProduct(new Product("Gizmo B", "GIZ-132", 5.50m, 3));

        // Order 2 (International)
        var addr2 = new Addresss("456 Elm Road", "Toronto", "ON", "Canada");
        var cust2 = new Customer("Aisha Khan", addr2);
        var order2 = new Order(cust2);
        order2.AddProduct(new Product("PowerPack", "PWR-777", 29.99m, 1));
        order2.AddProduct(new Product("Cable X", "CABL-21", 3.25m, 2));
        order2.AddProduct(new Product("Accessory Y", "ACC-09", 7.00m, 1));

        // Display Order 1
        Console.WriteLine("==== ORDER 1 ====");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Total Price: {order1.GetTotalPrice():C}");
        Console.WriteLine();

        // Display Order 2
        Console.WriteLine("==== ORDER 2 ====");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Total Price: {order2.GetTotalPrice():C}");
        Console.WriteLine();

        Console.WriteLine("End of demo. Press any key to exit.");
        Console.ReadKey();
    }
}

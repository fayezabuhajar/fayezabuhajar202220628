using System;
using System.Collections.Generic;

public class Cash : Payment
{
    public float cashTendered { get; set; }

    public Cash(float cashTendered)
    {
        this.cashTendered = cashTendered;
    }
}

public class Order
{
    public DateTime date { get; set; }
    public string status { get; set; }
    public List<OrderDetail> orderDetails { get; set; }

    public Order(DateTime date, string status)
    {
        this.date = date;
        this.status = status;
        this.orderDetails = new List<OrderDetail>();
    }

    public float calcSubTotal()
    {
        float subTotal = 0;
        foreach (var orderDetail in orderDetails)
        {
            subTotal += orderDetail.calcSubTotal();
        }
        return subTotal;
    }

    public float calcTax()
    {
        float tax = 0;
        foreach (var orderDetail in orderDetails)
        {
            tax += orderDetail.calcTax();
        }
        return tax;
    }

    public float calcTotal()
    {
        return calcSubTotal() + calcTax();
    }

    public float calcTotalWeight()
    {
        float totalWeight = 0;
        foreach (var orderDetail in orderDetails)
        {
            totalWeight += orderDetail.calcWeight();
        }
        return totalWeight;
    }
}

public abstract class Payment
{
    public float amount { get; set; }
}

public class Check : Payment
{
    public string name { get; set; }
    public string bankID { get; set; }

    public Check(string name, string bankID)
    {
        this.name = name;
        this.bankID = bankID;
    }

    public bool authorized()
    {
        // Perform the check here
        return true;
    }
}

public class OrderDetail
{
    public Item item { get; set; }
    public int quality { get; set; }
    public string taxStatus { get; set; }

    public OrderDetail(Item item, int quality, string taxStatus)
    {
        this.item = item;
        this.quality = quality;
        this.taxStatus = taxStatus;
    }

    public float calcSubTotal()
    {
        return item.getPriceForQuantity(quality);
    }

    public float calcWeight()
    {
        return item.shippingWeight * quality;
    }

    public float calcTax()
    {
        if (taxStatus == "Taxable")
        {
            return item.getTax() * quality;
        }
        return 0;
    }
}

public class Item
{
    public float shippingWeight { get; set; }
    public string description { get; set; }

    public Item(float shippingWeight, string description)
    {
        this.shippingWeight = shippingWeight;
        this.description = description;
    }

    public float getPriceForQuantity(int quantity)
    {
        // Retrieve the price from the database
        return 0;
    }

    public float getTax()
    {
        // Retrieve the tax rate from the database
        return 0;
    }

    public bool inStock()
    {
        // Check if the item is in stock
        return true;
    }
}

public class Credit : Payment
{
    public string name { get; set; }
    public string type { get; set; }
    public DateTime expDate { get; set; }

    public Credit(string name, string type, DateTime expDate)
    {
        this.name = name;
        this.type = type;
        this.expDate = expDate;
    }

    public bool authorized()
    {
        // Perform the credit card check here
        return true;
    }
}



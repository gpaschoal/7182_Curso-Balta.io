using System;
using System.Collections.Generic;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
  public class Order : Entity
  {
    public Order(Customer customer, DateTime date, decimal deliveryFee, Discount discount)
    {
      Customer = customer;
      Date = date;
      Number = Guid.NewGuid().ToString().Substring(0, 8);
      Items = new List<OrderItem>();
      DeliveryFee = deliveryFee;
      Discount = discount;
      Status = EOrderStatus.WaitingPayment;
    }

    public Customer Customer { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public IList<OrderItem> Items { get; set; }
    public decimal DeliveryFee { get; set; }
    public Discount Discount { get; set; }
    public EOrderStatus Status { get; set; }

    public void AddItem(Product product, int quantity)
    {
      var item = new OrderItem(product, quantity);
      Items.Add(item);
    }
  }
}
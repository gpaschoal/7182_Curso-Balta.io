using Flunt.Validations;
using Store.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        public Order(Customer customer, decimal deliveryFee, Discount discount)
        {
            AddNotifications(
              new Contract()
              .Requires()
                .IsNotNull(customer, "Customer", "Cliente inválido")
                .IsGreaterThan(deliveryFee, 0, "DeliveryFee", "")
            );

            Customer = customer;
            Date = DateTime.Now.Date;
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
            if (item.Valid)
                Items.Add(item);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Total();
            }

            total += DeliveryFee;
            total -= Discount != null ? Discount.Value() : 0;

            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total())
                Status = EOrderStatus.WaitingDelivery;
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
        }
    }
}
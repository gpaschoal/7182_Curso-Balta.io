using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;
using System;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrdertTests
    {
        private readonly Customer _customer;
        private readonly Product _product;
        private readonly Discount _discountValid;
        private readonly Discount _discountExpired;

        public OrdertTests()
        {
            _customer = new Customer("guilherme", "g@g.com");
            _product = new Product("Produto 1", 10, true);
            _discountValid = new Discount(10, DateTime.Now.AddDays(1).Date);
            _discountExpired = new Discount(10, DateTime.Now.AddDays(-1).Date);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres()
        {
            var order = new Order(_customer, 40, null);
            Assert.AreEqual(order.Number.Length, 8);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_o_status_deve_ser_aguardando_pagamento()
        {
            var order = new Order(_customer, 40, null);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_o_status_deve_ser_aguardando_entrega()
        {
            var order = new Order(_customer, 40, null);
            order.AddItem(_product, 1);
            order.Pay(50);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_o_status_deve_ser_cancelado()
        {
            var order = new Order(_customer, 40, null);
            order.Cancel();
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 40, null);
            order.AddItem(null, 30);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_com_quantidade_zero_ou_menos_nao_adicionar()
        {
            var order = new Order(_customer, 40, null);
            order.AddItem(null, 0);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_seu_total_deve_ser_50()
        {
            var order = new Order(_customer, 20, _discountValid);
            order.AddItem(_product, 4);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_expirado_seu_valor_deve_ser_60()
        {
            var order = new Order(_customer, 20, _discountExpired);
            order.AddItem(_product, 4);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_invalido_seu_valor_deve_ser_60()
        {
            var order = new Order(_customer, 20, null);
            order.AddItem(_product, 4);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_de_10_seu_valor_deve_ser_50()
        {
            var order = new Order(_customer, 20, _discountValid);
            order.AddItem(_product, 4);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_uma_taxa_de_entrega_de_10_o_valor_deve_ser_60()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
        {
            var order = new Order(null, 10, null);
            Assert.AreEqual(order.Invalid, true);
        }
    }
}
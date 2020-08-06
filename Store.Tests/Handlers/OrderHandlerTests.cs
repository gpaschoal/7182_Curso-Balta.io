using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;
using System;
using System.Reflection.Metadata;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _productRepository = new FakeProductRepository();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dados_um_cliente_invalido_o_pedido_nao_deve_ser_gerado()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dados_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dados_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_pedido_nao_deve_ser_gerado()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var command = new CreateOrderCommand
            {
                Customer = "12345678",
                ZipCode = "12345678",
                PromoCode = "12345678"
            };
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));

            var handler = new OrderHandler(
                            _customerRepository,
                            _deliveryFeeRepository,
                            _discountRepository,
                            _productRepository,
                            _orderRepository);
            handler.Handle(command);

            Assert.AreEqual(handler.Valid, true);
        }
    }
}

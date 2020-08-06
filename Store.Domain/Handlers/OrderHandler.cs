using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;
using System.Linq;

namespace Store.Domain.Handlers
{
    public class OrderHandler :
            Notifiable,
            IHandle<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(
            ICustomerRepository customerRepository,
            IDeliveryFeeRepository deliveryFeeRepository,
            IDiscountRepository discountRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido Inálido", command.Notifications);

            // 1. Recupera o cliente
            var customer = _customerRepository.Get(command.Customer);

            // 2. Calcula a taxa de entrega
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

            // 3. Obtém o cupom de desconto
            var discount = _discountRepository.Get(command.PromoCode);

            // 4. Gera o pedido
            var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
            //var products = _productRepository.Get(command.Items.Select(x => x.Product)).ToList(); //Eu faria desta forma
            var order = new Order(customer, deliveryFee, discount);
            foreach (var item in command.Items)
            {
                var product = products.FirstOrDefault(x => x.Id == item.Product);
                order.AddItem(product, item.Quantity);
            }

            // 5. Agrupa as notificações
            AddNotifications(customer.Notifications);
            AddNotifications(order.Notifications);

            // 6. Verifica se deu tudo certo
            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar Pedido", Notifications);

            // 7. Retorna o valor
            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
        }
    }
}

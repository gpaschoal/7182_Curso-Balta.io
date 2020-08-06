using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "")
                return new Customer("Gui Paschoal", "g@g.com");
            return null;
        }
    }
}

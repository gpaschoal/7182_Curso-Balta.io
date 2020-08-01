using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Repositories
{
    interface ICustomerRepository
    {
        Customer Get(string document);
    }
}

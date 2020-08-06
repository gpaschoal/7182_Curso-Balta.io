using Store.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Store.Domain.Utils
{
    public static class ExtractGuids
    {
        public static IEnumerable<Guid> Extract(IList<CreateOrderItemCommand> items)
        {
            var guids = new List<Guid>();
            foreach (var item in items)
                guids.Add(item.Product);
            return guids;
        }
    }
}

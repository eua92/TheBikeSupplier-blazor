using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Models.Client.ViewModels
{
    public class BasketInfo
    {
        public Guid? Id { get; set; }
        public int Count { get; set; } = 0;
    }
}

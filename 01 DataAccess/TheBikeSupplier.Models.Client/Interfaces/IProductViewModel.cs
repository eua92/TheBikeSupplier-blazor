using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Models.Client.Interfaces
{
    public interface IProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageSrc { get; set; }
        public byte[] ImageFileContent { get; set; }
    }
}

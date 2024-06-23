using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Models.Client.Interfaces;

namespace TheBikeSupplier.Models.Client.ViewModels
{
    public class WishListViewModel
    {
        public Guid Id { get; set; }
        public List<WishListItemViewModel> WishListItems { get; set; } = new();
    }

    public class WishListItemViewModel : IProductViewModel
    {
        public Guid Id { get; set; }
        public Guid WishListId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageSrc { get; set; } = "";
        public byte[] ImageFileContent { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;

namespace TheBikeSupplier.Models.Client
{
    public class OrderViewModel
    {
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
        public IEnumerable<OrderAddressViewModel> OrderAddresses { get; set; } 
    }

    public class OrderDetailViewModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderAddressViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string Country { get; set; }
        public AddressType Type { get; set; }
        public Guid OrderId { get; set; }
    }
}

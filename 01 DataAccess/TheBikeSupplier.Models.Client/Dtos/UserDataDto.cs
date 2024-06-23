using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Models.Client.Dtos
{
    public class UserDataDto
    {
        public Guid OwnerId { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}

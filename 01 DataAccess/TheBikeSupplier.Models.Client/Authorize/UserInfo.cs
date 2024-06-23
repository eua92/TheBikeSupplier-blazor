using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Models.Client.Authorize
{
    public class UserInfo
    {
        public bool IsAuthenticated { get; set; } = false;
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}

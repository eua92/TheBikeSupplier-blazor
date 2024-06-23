using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Common.Models
{
    public class NavBarLink
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = false;
    }
}

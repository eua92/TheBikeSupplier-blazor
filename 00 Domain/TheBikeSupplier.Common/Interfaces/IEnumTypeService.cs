using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Extensions;

namespace TheBikeSupplier.Common.Interfaces
{
    public interface IEnumTypeService
    {
        IEnumerable<DropdownEnumItemModel<TEnum>> GetDropDownModel<TEnum>() where TEnum : struct;
    }
}

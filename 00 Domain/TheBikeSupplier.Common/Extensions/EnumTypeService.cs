using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Interfaces;

namespace TheBikeSupplier.Common.Extensions
{
    public class EnumTypeService : IEnumTypeService
    {
        public IEnumerable<DropdownEnumItemModel<TEnum>> GetDropDownModel<TEnum>() where TEnum : struct
        {
            var values = Enum.GetValues(typeof(TEnum));

            return from TEnum type in values
                   select new DropdownEnumItemModel<TEnum>
                   {
                       Value = type,
                       Name = EnumExtensions<TEnum>.GetDescription(type)
                   };
        }
    }

    public class DropdownEnumItemModel<TEnum> where TEnum : struct
    {
        public TEnum Value { get; set; }
        public string Name { get; set; }
    }
}

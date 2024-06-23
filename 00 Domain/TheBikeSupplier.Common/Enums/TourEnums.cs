using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Resources;

namespace TheBikeSupplier.Common.Enums
{
    public enum TourLevel { Easy = 0, Moderate = 1, Hard = 2 }
    public enum TourDurationType 
    {
        [Display(ShortName = "HoursShort", ResourceType = typeof(StringResources))]
        Hours = 0, 
        Days = 1, 
        Weeks = 2 
    }
    public enum TourDistanceUnit 
    {
        [Display(ShortName = "KilometersShort", ResourceType = typeof(StringResources))]
        Kilometers = 0,
        [Display(ShortName = "MilesShort", ResourceType = typeof(StringResources))]
        Miles = 1
    }
}

using System.Collections.Generic;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Common.Models;
using TheBikeSupplier.Resources;

namespace TheBikeSupplier.Common
{
    public static class SiteLinks
    {
        public static IList<NavBarLink> MenuLinks = new List<NavBarLink>()
        {
            new NavBarLink
            {
                Link = "home",
                Name = StringResources.Home
            },
            new NavBarLink
            {
                Link = "bikes",
                Name = StringResources.Bikes
            },
            new NavBarLink
            {
                Link = "rent",
                Name = StringResources.Rent
            },
            new NavBarLink
            {
                Link = "repair",
                Name = StringResources.Repair
            },
            new NavBarLink
            {
                Link = "tours",
                Name = StringResources.Tours
            },
            new NavBarLink
            {
                Link = "about",
                Name = StringResources.About
            },
            new NavBarLink
            {
                Link = "contact",
                Name = StringResources.Contact
            }
        };

        public static IList<NavBarLink> AccountLinks = new List<NavBarLink>()
        {
            new NavBarLink
            {
                Link = "login",
                Name = StringResources.Login
            },
            new NavBarLink
            {
                Link = "createAccount",
                Name = StringResources.CreateAccount
            },
            new NavBarLink
            {
                Link = "wishList",
                Name = StringResources.WishList
            },
            new NavBarLink
            {
                Link = $"basket/{(int)CheckoutSteps.Basket}",
                Name = StringResources.Basket
            }
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;

namespace TheBikeSupplier.Web.Client.Services.Interfaces
{
    public interface IToasterService
    {
        void ShowToaster(string message, ToastType toastType, string title = null);
    }
}

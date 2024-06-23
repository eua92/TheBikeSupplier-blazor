using Sotsera.Blazor.Toaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Web.Client.Services.Interfaces;

namespace TheBikeSupplier.Web.Client.Services.Implementations
{
    public class ToasterService : IToasterService
    {
        private readonly IToaster _toaster;
        public ToasterService(IToaster toaster)
        {
            _toaster = toaster;
        }

        public void ShowToaster(string message, ToastType toastType, string title = null)
        {
            var libToastType = (Sotsera.Blazor.Toaster.Core.Models.ToastType)toastType;
            _toaster.Add(libToastType, message, title, conf =>
            {

            });
        }
    }
}

using CurrieTechnologies.Razor.SweetAlert2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;

namespace TheBikeSupplier.Web.Client.Services.Interfaces
{
    public interface IDialogService
    {
        Task<SweetAlertResult> ShowConfirm(string message, DialogType dialogType, bool showIcon = true, bool showTitle = true);
        Task ShowMessage(string message, DialogMessageType dialogMessageType, bool isHtml = false, bool showIcon = true);
    }
}

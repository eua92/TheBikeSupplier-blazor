using CurrieTechnologies.Razor.SweetAlert2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Web.Client.Services.Interfaces;

namespace TheBikeSupplier.Web.Client.Services.Implementations
{
    public class DialogService : IDialogService
    {
        private readonly SweetAlertService _sweetAlertService;
        public DialogService(SweetAlertService sweetAlertService)
        {
            _sweetAlertService = sweetAlertService;
        }

        public async Task<SweetAlertResult> ShowConfirm(string message, DialogType dialogType, bool showIcon = true, bool showTitle = true)
        {
            string confirmButtonText;
            string cancelButtonText;
            switch (dialogType)
            {
                case DialogType.OkCancel:
                    confirmButtonText = "Ok";
                    cancelButtonText = "Cancel";
                    break;
                case DialogType.YesNo:
                    confirmButtonText = "Yes";
                    cancelButtonText = "No";
                    break;
                default:
                    throw new Exception("Unkown Dialog Type");
            }
            return await ShowConfirmation("Question", message, confirmButtonText, cancelButtonText, true, true);
        }

        public async Task ShowMessage(string message, DialogMessageType dialogMessageType, bool isHtml = false, bool showIcon = true)
        {
            string title = string.Empty;
            SweetAlertIcon icon = null;
            switch (dialogMessageType)
            {
                case DialogMessageType.Info:
                    title = "Info";
                    icon = SweetAlertIcon.Info;
                    break;
                case DialogMessageType.Success:
                    title = "Success";
                    icon = SweetAlertIcon.Success;
                    break;
                case DialogMessageType.Warn:
                    title = "Warning";
                    icon = SweetAlertIcon.Warning;
                    break;
                case DialogMessageType.Error:
                    title = "Error";
                    icon = SweetAlertIcon.Error;
                    break;
                case DialogMessageType.None:
                    title = null;
                    icon = null;
                    break;
                default:
                    break;
            }

            if (isHtml)
            {
                await ShowAlert(title, showIcon ? icon : null, null, message, "Ok");
            }
            else
            {
                await ShowAlert(title, showIcon ? icon : null, message, null, "Ok");
            }
        }

        private async Task<SweetAlertResult> ShowConfirmation(string title, string text, string confirmButtonText, string cancelButtonText,
            bool showIcon = true, bool showTitle = true)
        {
            var options = new SweetAlertOptions
            {
                Title = showTitle ? title : null,
                Text = text,
                Icon = showIcon ? SweetAlertIcon.Question : null,
                Width = "520",
                ShowCancelButton = true,
                AllowOutsideClick = false,
                ConfirmButtonText = confirmButtonText,
                CancelButtonText = cancelButtonText
            };
            return await _sweetAlertService.FireAsync(options);
        }

        private async Task ShowAlert(string title, SweetAlertIcon icon, string text, string html, string buttonText)
        {
            var options = new SweetAlertOptions
            {
                Title = title,
                Text = text,
                Icon = icon,
                AllowOutsideClick = false,
                ConfirmButtonText = buttonText,
            };

            if (!string.IsNullOrWhiteSpace(text))
            {
                options.Text = text;
            }

            if (!string.IsNullOrWhiteSpace(html))
            {
                options.Html = html;
            }
            await _sweetAlertService.FireAsync(options);
        }
    }
}

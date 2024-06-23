using System;
using System.IO;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Client.Interfaces;
using TheBikeSupplier.Resources;

namespace TheBikeSupplier.DataServices.Implementations
{
    public class ProductImageStorage<TModel, TViewModel> : IProductImageStorage<TModel, TViewModel>
        where TModel : Product
        where TViewModel : IProductViewModel
    {
        private readonly IAzureStorageService _azureStorageService;

        public ProductImageStorage(IAzureStorageService azureStorageService)
        {
            _azureStorageService = azureStorageService;
        }

        public async Task<OperationResult<string>> UpdateProductImage(TViewModel viewModel, TModel model)
        {
            if (viewModel.ImageFileContent != null)
            {
                var result = await _azureStorageService.UploadFile("thebikesupplier", model.Id.ToString(), new MemoryStream(viewModel.ImageFileContent));
                if (result.Failed)
                    return OperationResult.CreateFailure<string>(StringResources.FailedToUploadImage);

                return OperationResult.CreateSuccess(result.Result);
            }
            else if (string.IsNullOrWhiteSpace(viewModel.ImageSrc) && !string.IsNullOrWhiteSpace(model.ImageSrc))
            {
                await _azureStorageService.DeleteFile("thebikesupplier", model.Id.ToString());
                return OperationResult.CreateSuccess(string.Empty);
            }

            return OperationResult.CreateSuccess(viewModel.ImageSrc);
        }

        public async Task<OperationResult> DeleteProductImage(Guid productId)
        {
            return await _azureStorageService.DeleteFile("thebikesupplier", productId.ToString());
        }
    }
}

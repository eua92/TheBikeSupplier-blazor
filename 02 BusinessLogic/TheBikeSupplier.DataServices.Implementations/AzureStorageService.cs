using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.Resources;

namespace TheBikeSupplier.DataServices.Implementations
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<OperationResult<string>> UploadFile(string containerName, string fileName, Stream file)
        {
            try
            {
                var container = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = container.GetBlobClient(fileName);
                await blobClient.UploadAsync(file, new BlobHttpHeaders { ContentType = "image/png" });

                return OperationResult.CreateSuccess(blobClient.Uri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<string>(ex.Message);
            }
        }

        public async Task<OperationResult> DeleteFile(string containerName, string filename)
        {
            try
            {
                var container = _blobServiceClient.GetBlobContainerClient(containerName);
                var response = await container.DeleteBlobIfExistsAsync(filename);                
                if (!response.Value)
                    return OperationResult.CreateFailure(StringResources.FileNotFound);            

                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }
    }
}

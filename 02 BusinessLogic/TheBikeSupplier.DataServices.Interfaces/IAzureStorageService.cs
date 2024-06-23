using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common;

namespace TheBikeSupplier.DataServices.Interfaces
{
    public interface IAzureStorageService
    {
        Task<OperationResult<string>> UploadFile(string containerName, string fileName, Stream file);
        Task<OperationResult> DeleteFile(string containerName, string filename);
    }
}

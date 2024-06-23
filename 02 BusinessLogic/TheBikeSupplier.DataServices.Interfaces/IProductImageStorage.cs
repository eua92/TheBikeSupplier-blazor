using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common;

namespace TheBikeSupplier.DataServices.Interfaces
{
    public interface IProductImageStorage<TModel, TViewModel>
    {
        Task<OperationResult<string>> UpdateProductImage(TViewModel viewModel, TModel model);
        Task<OperationResult> DeleteProductImage(Guid productId);
    }
}

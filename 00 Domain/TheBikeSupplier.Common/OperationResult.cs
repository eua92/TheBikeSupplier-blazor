using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Common
{
    public class OperationResult
    {
        protected OperationResult(bool success, string errorMessage)
        {
            this.Succeeded = success;
            this.ErrorMessage = errorMessage;
        }

        public bool Succeeded { get; private set; }
        public bool Failed { get => !Succeeded; }
        public string ErrorMessage { get; protected set; }

        public static OperationResult CreateSuccess()
        {
            return new OperationResult(true, null);
        }

        public static OperationResult<TResult> CreateSuccess<TResult>(TResult result)
        {
            return new OperationResult<TResult>(result, true, null);
        }

        public static OperationResult CreateFailure(string errorMessage)
        {
            return new OperationResult(false, errorMessage);
        }

        public static OperationResult<TResult> CreateFailure<TResult>(string errorMessage)
        {
            return new OperationResult<TResult>(default, false, errorMessage);
        }
    }

    public class OperationResult<TResult> : OperationResult
    {
        public OperationResult(TResult result, bool success, string errorMessage)
            : base(success, errorMessage)
        {
            this.Result = result;
        }

        public TResult Result { get; private set; }
    }
}

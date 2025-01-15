using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.Common.Result
{
    public class Result
    {
        public bool IsSuccess { get; }

        public CustomError Error { get; }

        protected Result(bool isSuccess, CustomError error)
        {
            if (isSuccess && error != CustomError.None ||
                !isSuccess && error == CustomError.None)
            {
                throw new ArgumentException("Invalid Operation", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new (true, CustomError.None);
        public static Result Failure(CustomError error) => new (false, error);
    }

    public class Result<T> : Result
    {

        public T Data { get; }

        private Result(T value, bool isSuccess, CustomError error) : base(isSuccess, error)
        {
            Data = value;
        }       
        
        public static Result<T> Success(T value) => new (value, true, CustomError.None);
        public new static Result<T> Failure(CustomError error) => new (default, false, error);
 
    }


}

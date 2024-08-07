using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.Common.Result
{
    public class Result
    {
        public bool IsSuccess { get; }
        //public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalida error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Sucess() => new (true, Error.None);
        //public static Result<TValue> Sucess<TValue>(TValue value) => new (value,true, Error.None);
        public static Result Failure(Error error) => new (false, error);
    }


}

﻿using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.Common.Result
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalida Operation", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new (true, Error.None);
        public static Result Failure(Error error) => new (false, error);
    }

    public class Result<T> : Result
    {

        public T Data { get; }

        private Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Data = value;
        }       
        private Result(T value, bool isSuccess, Error error, List<string?> erros) : base(isSuccess, error)
        {
            Data = value;
        }
        
        public static Result<T> Success(T value) => new Result<T>(value, true, Error.None);
        public new static Result<T> Failure(Error error) => new Result<T>(default(T), false, error);
        public new static Result<T> Failure(Error erro, List<string?> erros) => new Result<T>(default(T), default, erro, erros);
    }


}

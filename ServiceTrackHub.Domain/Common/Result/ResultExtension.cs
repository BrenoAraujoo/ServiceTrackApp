using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.Common.Result
{
    public static class ResultExtension
    {
        public static T Match<T>(
            this Result result,
            Func<T> onSuccess,
            Func<Error,T> onFailure
            )
        {
            return result.IsSuccess? onSuccess():
                onFailure(result.Error);
        }
    }
}

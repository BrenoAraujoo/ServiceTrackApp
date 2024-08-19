namespace ServiceTrackHub.Domain.Common.Erros
{


    public sealed record CustomError (string Code, string Message, List<string?> erros)
    {

        public static readonly CustomError None = new(string.Empty, string.Empty, new List<string?>());
        public static readonly string RecordNotFoundCode = "RecordNotFound";
        public static readonly string ValidationErrorCode = "ValidationError";
        
        public CustomError(string code, string message) : this(code, message, new List<string?>())
        {
        }
        public static CustomError RecordNotFound(string message)
        {
            return new CustomError(RecordNotFoundCode, message);
        }
        public static CustomError ValidationError(string message)
        {
            return new CustomError(ValidationErrorCode, message);
        }

    }
}

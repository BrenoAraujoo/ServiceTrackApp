namespace ServiceTrackHub.Domain.Common.Erros
{


    public sealed record CustomError (int Code, string Message)
    {

        public static readonly CustomError None = new(0, string.Empty);

        private const int RecordNotFoundCode = 1;
        private const int ValidationErrorCode = 2;
        private const int ConflictErrorCode = 3;
        private const int AuthenticationErrorCode = 4;
        private const int ServerErrorCode = 5;
        
        public static CustomError RecordNotFound(string message) => new (RecordNotFoundCode, message);
        public static CustomError ValidationError(string message) 
            =>new (ValidationErrorCode, message);

        public static CustomError Conflict (string message) => new (ConflictErrorCode, message);

        public static CustomError AuthenticationError(string message) => new (AuthenticationErrorCode, message);
        
        public static CustomError ServerError(string message) => new (ServerErrorCode, message);
    }
}

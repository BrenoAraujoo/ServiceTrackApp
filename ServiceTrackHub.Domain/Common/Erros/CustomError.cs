namespace ServiceTrackHub.Domain.Common.Erros
{


    public sealed record CustomError (int Code, string Message)
    {

        public static readonly CustomError None = new(0, string.Empty);

        private static readonly int RecordNotFoundCode = 1;
        private static readonly int ValidationErrorCode = 2;
        private static readonly int ConflictErrorCode = 3;
        private static readonly int AuthenticationErrorCode = 4;
        private static readonly int ServerErrorCode = 5;
        
        public static CustomError RecordNotFound(string message) => new (RecordNotFoundCode, message);
        public static CustomError ValidationError(string message) 
            =>new (ValidationErrorCode, message);

        public static CustomError Conflict (string message) => new (ConflictErrorCode, message);

        public static CustomError AuthenticationError(string message) => new (AuthenticationErrorCode, message);
        
        public static CustomError ServerError(string message) => new (ServerErrorCode, message);
    }
}

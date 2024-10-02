namespace ServiceTrackHub.Domain.Common.Erros
{


    public sealed record CustomError (int Code, string Message, List<string> Erros)
    {

        public static readonly CustomError None = new(0, string.Empty, new List<string>());

        public static readonly int RecordNotFoundCode = 1;
        public static readonly int ValidationErrorCode = 2;
        public static readonly int ConflictErrorCode = 3;
        public static readonly int AuthenticationErrorCode = 4;
        
        public CustomError( int code, string message) : this(code, message, new List<string>())
        {
        }
        public static CustomError RecordNotFound(string message) => new (RecordNotFoundCode, message);
        public static CustomError ValidationError(string message, List<string> erros = null) 
            =>new (ValidationErrorCode, message, erros ?? new List<string>());

        public static CustomError Conflict (string message, List<string> erros = null) => new (ConflictErrorCode, message, erros ?? new List<string>());

        public static CustomError AuthenticationError(string message) => new (AuthenticationErrorCode, message);

    }
}

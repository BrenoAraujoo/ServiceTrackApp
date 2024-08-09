namespace ServiceTrackHub.Domain.Common.Erros
{


    public sealed record Error (string Code, string Description, List<string?> erros)
    {

        public static readonly Error None = new(string.Empty, string.Empty, new List<string?>());
        public Error(string code, string description) : this(code, description, new List<string?>())
        {
        }

    }
}

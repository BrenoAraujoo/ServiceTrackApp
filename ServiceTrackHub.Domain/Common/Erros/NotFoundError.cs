namespace ServiceTrackHub.Domain.Common.Erros
{
    public static class NotFoundError
    {
        public static  Error NotFound(int? id) => new Error("Code", $"User with id {id} not found");
        
    }
}

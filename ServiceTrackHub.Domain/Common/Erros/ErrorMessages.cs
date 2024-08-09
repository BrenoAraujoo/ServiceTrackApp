namespace ServiceTrackHub.Domain.Common.Erros
{
    public static class ErrorMessages
    {
        public static  Error NotFound(Guid? id, string entity) => new Error("404", $"{entity} with id {id} not found");
        public static  Error NotFound(string entity) => new Error("404",$"{entity} not found");
        public static  Error BadRequest(string entity, List<string?> erros) => new Error("400", $"{entity} is invalid", erros);
        public static  Error BadRequest(string entity) => new Error("400", $"{entity} is invalid");
        
    }
}

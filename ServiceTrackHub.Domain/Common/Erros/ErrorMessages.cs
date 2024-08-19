namespace ServiceTrackHub.Domain.Common.Erros
{
    public static class ErrorMessages
    {
        public static  CustomError NotFound(Guid? id, string entity) => new CustomError("404", $"{entity} with id {id} not found");
        public static  CustomError NotFound(string entity) => new CustomError("404",$"{entity} not found");
        public static  CustomError BadRequest(string entity, List<string?> erros) => new CustomError("400", $"{entity} is invalid", erros);
        public static  CustomError BadRequest(string entity) => new CustomError("400", $"{entity} is invalid");
        
    }
}

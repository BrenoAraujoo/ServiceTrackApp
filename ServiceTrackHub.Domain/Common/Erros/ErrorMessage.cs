namespace ServiceTrackHub.Domain.Common.Erros
{
    public sealed class ErrorMessage
    {
        #region User messages
        public static readonly string UserNotFound = "O Usuário com  Id {0} não foi encontrado";
        public static readonly string UserEmailAlreadyExists = "O email {0} já existe. Tente usar outro.";
        public static readonly string UserInvalid = "Usuário inválido";
      
        #endregion


    }
}

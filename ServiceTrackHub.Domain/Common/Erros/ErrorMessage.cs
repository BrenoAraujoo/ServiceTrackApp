namespace ServiceTrackHub.Domain.Common.Erros
{
    public sealed class ErrorMessage
    {
        #region User messages
        public static readonly string UserNotFound = "O Usuário com  Id \"{0}\" não foi encontrado";
        public static readonly string UserEmailAlreadyExists = "O email \"{0}\" já existe. Tente usar outro.";
        public static readonly string UserInvalid = "Usuário inválido";
        public static readonly string UserInvalidPasswordHash = "Erro ao gerar hash do password";
        public static readonly string UserCannotBeRemove = "O usuário com Id \"{0}\" não pode ser removido," +
                                                           "pois ele possui tarefas associadas.";

        #endregion

        #region Task messages
        public static readonly string TaskNotFound = "A tarefa com o id \"{0}\"não foi encontrada";
        public static readonly string TaskInvalid = "Tarefa inválida";
        public static readonly string TaskNameAlreadyExists = "Já existe uma tarefa com o nome \"{0}\"." +
            " Por favor, tente com outro nome";
        #endregion

        #region Task Type messages
        public static readonly string TaskTypeNotFound = "O tipo de tarefa com id \"{0}\" não foi encontrado";
        public static readonly string TaskTypeInvalid= "Tipo de tarefa inválido";
        public static readonly string TaskTypeCantBeRemoved= "O tipo de tarefa {0} não pode ser removida pois está associada " +
                                                             "a uma ou mais tarefas.";
        #endregion


    }
}

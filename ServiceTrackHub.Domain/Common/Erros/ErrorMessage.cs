namespace ServiceTrackHub.Domain.Common.Erros
{
    public sealed class ErrorMessage
    {
        #region User messages
        public const string UserNotFound = "O Usuário com  Id \"{0}\" não foi encontrado";
        public const string UserEmailAlreadyExists = "O email \"{0}\" já existe. Tente usar outro.";
        public const string UserInvalid = "Usuário inválido";
        public const string UserErrorPasswordHash = "Erro ao gerar hash do password";
        public const string UserCannotBeRemove = "O usuário com Id \"{0}\" não pode ser removido," +
                                                           "pois ele possui tarefas associadas.";
        public const string UserInvalidEmailOrPassword = "Usuário ou senha inválidos";

        public const string UserIsAlreadyActivated = "O usuário já está ativo";
        public const string UserIsAlreadyInactivated = "O usuário já está inativo";

        #endregion

        #region Task messages
        public const string TaskNotFound = "A tarefa com o id \"{0}\"não foi encontrada";
        public const string TaskInvalid = "Tarefa inválida";
        public const string TaskNameAlreadyExists = "Já existe uma tarefa com o nome \"{0}\"." +
                                                    " Por favor, tente com outro nome";
        #endregion

        #region Task Type messages
        public const string TaskTypeNotFound = "O tipo de tarefa com id \"{0}\" não foi encontrado";
        public const string TaskTypeInvalid= "Tipo de tarefa inválido";
        public const string TaskTypeCantBeRemoved= "O tipo de tarefa {0} não pode ser removida pois está associada " +
                                                   "a uma ou mais tarefas.";
        #endregion

        #region Value objects messages

        public const string InvalidPhone = "O campo  obrigatório 'telefone' não foi enviado ou está em um formato inválido. " +
                                                     "Verifique o formato e o tamanho, que deve possui 11 digitos.";
        public const string InvalidEmail = "O campo obrigatório 'email' não foi enviado ou está em um formato inválido.";
        
        public const string InvalidPassword = "O campo obrigatório 'password' não foi enviado ou está em um formato inválido.";


        #endregion
    }
}

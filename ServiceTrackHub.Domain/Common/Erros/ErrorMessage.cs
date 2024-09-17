namespace ServiceTrackHub.Domain.Enums.Common.Erros
{
    public sealed class ErrorMessage
    {
        #region User messages
        public const string UserNotFound = $"O Usuário não encontrado";
        public const string UserEmailAlreadyExists = "Email já está em uso. Tente usar outro.";
        public const string UserInvalid = "Usuário inválido";
        public const string UserErrorPasswordHash = "Erro ao gerar hash do password";
        public const string UserCannotBeRemove = "O usuário não pode ser removido," +
                                                           "pois ele possui tarefas associadas.";
        public const string UserInvalidEmailOrPassword = "Usuário ou senha inválidos";

        public const string UserIsAlreadyActivated = "O usuário já está ativo";
        public const string UserIsAlreadyInactivated = "O usuário já está inativo";

        #endregion

        #region Task messages
        public const string TaskNotFound = "Tarefa não foi encontrada";
        public const string TaskInvalid = "Tarefa inválida";
        public const string TaskNameAlreadyExists = "Já existe uma tarefa com esse nome." +
                                                    " Por favor, tente com outro nome";

        public const string TaskAlreadyActivated = "A tarefa já está ativa";
        public const string TaskAlreadyInactivated = "A tarefa já está inaativa";
        #endregion

        #region Task Type messages
        public const string TaskTypeNotFound = "Tipo de tarefa não encontrado";
        public const string TaskTypeInvalid= "Tipo de tarefa inválido";
        public const string TaskTypeCantBeRemoved= "Esse tipo de tarefa não pode ser removida pois está associada " +
                                                   "a uma ou mais tarefas.";
        #endregion

        #region Value objects messages

        public const string InvalidPhone = "O campo  obrigatório 'telefone' não foi enviado ou está em um formato inválido. " +
                                                     "Verifique o formato e o tamanho, que deve possui 11 digitos.";
        public const string InvalidEmail = "O campo obrigatório 'email' não foi enviado ou está em um formato inválido.";
        
        public const string InvalidPassword = "O campo obrigatório 'password' não foi enviado ou está em um formato inválido.";

        public const string InvalidJobPosition = "O cargo deve ter no máximo 40 caracteres.";

        #endregion
    }
}

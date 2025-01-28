namespace ServiceTrackApp.Domain.Common.Erros
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

        public const string UserIsAlreadyActivated = "O usuário já está ativo";
        public const string UserIsAlreadyInactivated = "O usuário já está inativo";

        public const string UserRoleNotFound = "Tipo de usuário não encontrado";

        #endregion

        #region Customer messages

        public const string CustomerNotFound = "Cliente não encontrado";
        

        #endregion
        
        #region Task messages
        public const string TaskNotFound = "Tarefa não foi encontrada";
        public const string TaskInvalid = "Tarefa inválida";
        public const string TaskNameAlreadyExists = "Já existe uma tarefa com esse nome." +
                                                    " Por favor, tente com outro nome";

        public const string TaskInvalidUser = "A tarefa só pode ser atribuída a um usuário ativo.";

        public const string TaskUserNotFound = "O usuário cujo a tarefa está sendo atribuida não foi existe.";

        public const string TaskMaxDescription = "A descrição da tarefa deve ter no máximo 100 caracteres.";
        public const string TaskMinDescription = "A descrição da tarefa deve ter no mínimo 3 caracteres.";

        #endregion

        #region Task Type messages
        public const string TaskTypeNotFound = "Tipo de tarefa não encontrado";
        public const string TaskTypeInvalidName= "O nome do tipo da tarefa não pode ser vazio";
        public const string TaskTypeCantBeRemoved= "Esse tipo de tarefa não pode ser removida pois está associada " +
                                                   "a uma ou mais tarefas.";
        public const string TaskTypeMaxName = "O nome do tipo de tarefa deve ter no máximo 100 caracteres.";
        public const string TaskTypeMinName = "O nome do tipo de tarefa deve ter no mínimo  3 caracteres";
        
        public const string TaskTypeAlreadyActivated = "O Tipo de tarefa já está ativo";
        public const string TaskTypeAlreadyInactivated = "O Tipo de tarefa já está inativo";
        #endregion

        #region Value objects messages

        public const string InvalidPhone = "O campo  obrigatório 'telefone' não foi enviado ou está em um formato inválido. " +
                                                     "Verifique o formato e o tamanho, que deve possui 11 digitos.";
        public const string InvalidEmail = "O campo obrigatório 'email' não foi enviado ou está em um formato inválido.";
        public const string InvalidEmailLeght = "O campo 'email' deve ter entre 8 e 50 caracteres.";
        
        public const string InvalidPassword = "O campo obrigatório 'senha' não foi enviado ou está em um formato inválido.";

        public const string InvalidJobPosition = "O cargo deve ter no máximo 40 caracteres.";

        public const string InvalidPostalCode = "O CEP está inválido ou vazio";

        #endregion

        #region Auth messages

        public const string InvalidEmailOrPassword = "Usuário ou senha inválidos";
        public const string InvalidRefreshToken = "Refresh Token está inválido ou vencido";
        public const string Unauthorized = "Acesso negado. Você não tem permissão para acessar este recurso.";
        public const string NotAuthenticated = "Acesso negado. Você não está autenticado. Faça login novamente.";

        #endregion

        #region BlobUploadService
        public const string UploadImageError = "Erro ao efetuar o upload da imagem. Tente novamente.";
        #endregion
    }
}

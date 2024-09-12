using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.Auth;

public record LoginModel(
    [EmailAddress(ErrorMessage = "Deve ser informado um e-mail válido")]
    string Email,
    string Password
);

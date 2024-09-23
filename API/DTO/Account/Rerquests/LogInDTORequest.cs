using FluentValidation;

namespace API.DTO.Account.Rerquests;

public class LogInDTORequest
{
    public string username{  get; set; }

    public string password{ get; set; }
}


public class LogInDTORequestValidator : AbstractValidator<LogInDTORequest>
{
    public LogInDTORequestValidator()
    {
        RuleFor(r => r.username).NotNull().NotEmpty();
        RuleFor(r => r.password).NotNull().NotEmpty();

    }
}
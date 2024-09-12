using FluentValidation;

namespace API.DTO.BookStore.Requests;

public class CreateBookDTORequest
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string ISBN { get; set; }
}

public class  CreateBookDTORequestValidator : AbstractValidator<CreateBookDTORequest>
{
    public CreateBookDTORequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.ISBN).NotEmpty().Length(13);

    }
}
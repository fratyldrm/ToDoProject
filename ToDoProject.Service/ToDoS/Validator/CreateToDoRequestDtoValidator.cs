using FluentValidation;
using ToDoProject.Model.ToDos.Dtos.Create.Request;

namespace ToDoProject.Service.ToDoS.Validator;

public class CreateToDoRequestDtoValidator : AbstractValidator<CreateToDoRequestDto>
{
    public CreateToDoRequestDtoValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty().WithMessage("ToDo başlığı zorunludur.")
           .Length(2, 30).WithMessage("ToDo başlığı 2 ile 30 karakter arasında olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("ToDo açıklaması boş bırakılamaz.")
            .Length(1, 512).WithMessage("Açıklama en az 1 karakter, en fazla 512 karakter olmalıdır.");

      

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Kategori kimliği 0'dan büyük olmalıdır.");
    }
}

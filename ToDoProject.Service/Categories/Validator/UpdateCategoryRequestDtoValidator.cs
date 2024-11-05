

using FluentValidation;
using ToDoProject.Model.Categories.Dtos.Update;

namespace ToDoProject.Service.Categories.Validator;

public class UpdateCategoryRequestDtoValidator : AbstractValidator<UpdateCategoryRequestDto>
{
    public UpdateCategoryRequestDtoValidator()
    {
        RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Category ismi boş geçilemez")
          .Length(2, 30).WithMessage("Category ismi 2 ile 30 karakter arasında olmalıdır");
    }
}

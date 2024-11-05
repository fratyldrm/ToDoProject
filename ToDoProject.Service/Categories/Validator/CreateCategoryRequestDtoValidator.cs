using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoProject.Model.Categories.Dtos.Create;

namespace ToDoProject.Service.Categories.Validator;

public class CreateCategoryRequestDtoValidator : AbstractValidator<CreateCategoryRequestDto>
{
    public CreateCategoryRequestDtoValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Category ismi boş geçilemez")
           .Length(2, 30).WithMessage("Category ismi 2 ile 30 karakter arasında olmalıdır");
    }
}

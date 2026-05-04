using FluentValidation;
using TaskManagerApi.DTOs;

namespace TaskManagerApi.Validators;

public class UpdateTaskDtoValidator: AbstractValidator<UpdateTaskDto>
{
    public UpdateTaskDtoValidator()
    {
        RuleFor(x =>  x.Title)
            .NotEmpty().WithMessage("Title is required!")
            .MaximumLength(100);
        
        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
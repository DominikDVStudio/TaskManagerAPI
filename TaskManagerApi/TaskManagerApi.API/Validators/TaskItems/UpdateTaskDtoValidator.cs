using FluentValidation;
using TaskManagerApi.DTOs.TaskItems;

namespace TaskManagerApi.Validators.TaskItems;

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
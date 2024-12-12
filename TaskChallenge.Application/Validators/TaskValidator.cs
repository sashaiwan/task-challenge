using FluentValidation;
using TaskChallenge.Infrastructure.Models;

namespace TaskChallenge.Application.Validators;

public class TaskValidator : AbstractValidator<TaskModel>
{
    public TaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Description)
            .MaximumLength(255);

        RuleFor(x => x.DueDate)
            .NotEmpty();
    }
}
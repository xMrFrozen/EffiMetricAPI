using FluentValidation;
using EffiMetricAPI.DTOs;
namespace EffiMetricAPI.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Score).InclusiveBetween(0, 100).WithMessage("Score must be between 0 and 100.");
        }
    }
}

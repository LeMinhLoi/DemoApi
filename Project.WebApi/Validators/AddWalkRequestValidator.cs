using FluentValidation;
using Project.WebApi.Models.DTO;

namespace Project.WebApi.Validators
{
    public class AddWalkRequestValidator: AbstractValidator<AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}

using FluentValidation;
using Project.WebApi.Models.DTO;

namespace Project.WebApi.Validators
{
    public class UpdateWalkRequestValidator: AbstractValidator<UpdateWalkRequest>
    {
        public UpdateWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}

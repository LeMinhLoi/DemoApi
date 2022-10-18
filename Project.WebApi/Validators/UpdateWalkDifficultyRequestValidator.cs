using FluentValidation;
using Project.WebApi.Models.DTO;

namespace Project.WebApi.Validators
{
    public class UpdateWalkDifficultyRequestValidator: AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}

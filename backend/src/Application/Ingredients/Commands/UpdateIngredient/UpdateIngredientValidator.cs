namespace SmartMealPlanner.Application.Ingredients.Commands.UpdateIngredient;

// We use FluentValidation validators that are auto-discovered from the Application assembly.
public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
{
    public UpdateIngredientCommandValidator()
    {   
        RuleFor(v => v.Id)
            .GreaterThan(0);

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Category)
            .IsInEnum();
    }
}
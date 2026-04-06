namespace SmartMealPlanner.Application.Ingredients.Commands.CreateIngredient;

// We use FluentValidation validators that are auto-discovered from the Application assembly.
public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Category)
            .IsInEnum();
    }
}
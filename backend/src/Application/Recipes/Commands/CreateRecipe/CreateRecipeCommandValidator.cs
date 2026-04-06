namespace SmartMealPlanner.Application.Recipes.Commands.CreateRecipe;

// We use FluentValidation validators that are auto-discovered from the Application assembly.
public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
    public CreateRecipeCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(v => v.Servings)
            .GreaterThan(0);
        
        RuleFor(v => v.PrepTimeMinutes)
            .GreaterThanOrEqualTo(0);

        RuleFor(v => v.CookTimeMinutes)
            .GreaterThanOrEqualTo(0);
    }
}
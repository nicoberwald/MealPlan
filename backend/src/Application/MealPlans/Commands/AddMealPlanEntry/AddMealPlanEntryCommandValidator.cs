namespace SmartMealPlanner.Application.MealPlans.Commands.AddMealPlanEntry;
// We use FluentValidation validators that are auto-discovered from the Application assembly.
public class AddMealPlanEntryCommandValidator : AbstractValidator<AddMealPlanEntryCommand>
{
    public AddMealPlanEntryCommandValidator()
    {
        RuleFor(v => v.MealPlanId)
            .GreaterThan(0);
        
        RuleFor(v => v.RecipeId)
            .GreaterThan(0);
        
        RuleFor(v => v.Day)
            .IsInEnum();
        
        RuleFor(v => v.MealType)
            .IsInEnum();
    }
}
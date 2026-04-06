namespace SmartMealPlanner.Application.MealPlans.Commands.CreateMealPlan;

// We use FluentValidation validators that are auto-discovered from the Application assembly.
public class CreateMealPlanCommandValidator : AbstractValidator<CreateMealPlanCommand>
{
    public CreateMealPlanCommandValidator()
    {
        RuleFor(v => v.WeekStartDate)
            .NotEmpty();
    }
}
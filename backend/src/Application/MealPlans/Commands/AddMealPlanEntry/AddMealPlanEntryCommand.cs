namespace SmartMealPlanner.Application.MealPlans.Commands.AddMealPlanEntry;

public record AddMealPlanEntryCommand : IRequest<int>
{
    public int MealPlanId { get; init; }
    public int RecipeId { get; init; }
    public WeekDay Day { get; init; }
    public MealType MealType { get; init; }
}
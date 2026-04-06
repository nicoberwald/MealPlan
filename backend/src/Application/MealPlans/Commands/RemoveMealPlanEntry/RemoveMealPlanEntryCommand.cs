namespace SmartMealPlanner.Application.MealPlans.Commands.RemoveMealPlanEntry;

public record RemoveMealPlanEntryCommand : IRequest
{
    public int Id { get; init; }

}
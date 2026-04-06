namespace SmartMealPlanner.Application.MealPlans.Commands.DeleteMealPlan;

public record DeleteMealPlanCommand : IRequest
{
    public int Id { get; init; }
}
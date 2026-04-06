namespace SmartMealPlanner.Application.MealPlans.Commands.CreateMealPlan;

public record CreateMealPlanCommand : IRequest<int>
{
    public DateOnly WeekStartDate { get; init; }
}
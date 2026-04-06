namespace SmartMealPlanner.Application.MealPlans.Queries.GetMealPlans;

public record GetMealPlansQuery : IRequest<IEnumerable<MealPlanDto>>;
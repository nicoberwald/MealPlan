namespace SmartMealPlanner.Application.MealPlans.Queries.GetMealPlanById;

public record GetMealPlanByIdQuery(int Id) : IRequest<MealPlanDetailDto>;
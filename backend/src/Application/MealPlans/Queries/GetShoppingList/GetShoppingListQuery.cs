namespace SmartMealPlanner.Application.MealPlans.Queries.GetShoppingList;

public record GetShoppingListQuery(int MealPlanId) : IRequest<IEnumerable<ShoppingListItemDto>>;
namespace SmartMealPlanner.Application.MealPlans.Queries.GetShoppingList;

public class ShoppingListItemDto
{
    public string Name { get; init; } = string.Empty;
    public decimal Quantity { get; init; }
    public MeasurementUnit Unit { get; init; }
}
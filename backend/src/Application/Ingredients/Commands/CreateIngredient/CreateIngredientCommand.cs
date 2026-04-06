namespace SmartMealPlanner.Application.Ingredients.Commands.CreateIngredient;

public record CreateIngredientCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public IngredientCategory Category { get; init; }
}
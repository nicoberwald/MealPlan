namespace SmartMealPlanner.Application.Ingredients.Commands.UpdateIngredient;

public record UpdateIngredientCommand : IRequest
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty; // Default er en tom string

    public IngredientCategory Category { get; init; }
}
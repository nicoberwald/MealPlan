namespace SmartMealPlanner.Application.Ingredients.Commands.DeleteIngredient;

public record DeleteIngredientCommand : IRequest
{
    public int Id { get; init; }
}
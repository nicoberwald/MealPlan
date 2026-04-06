namespace SmartMealPlanner.Application.Recipes.Commands.DeleteRecipe;

public record DeleteRecipeCommand : IRequest
{
    public int Id { get; init; }
}
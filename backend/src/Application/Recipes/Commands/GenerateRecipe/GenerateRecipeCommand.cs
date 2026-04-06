namespace SmartMealPlanner.Application.Recipes.Commands.GenerateRecipe;

public record GenerateRecipeCommand : IRequest<int>
{
    public string Prompt { get; init; } = string.Empty;
}
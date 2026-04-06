namespace SmartMealPlanner.Application.Recipes.Queries.GetRecipes;

public record GetRecipesQuery : IRequest<IEnumerable<RecipeDto>>;
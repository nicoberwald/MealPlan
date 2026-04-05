namespace SmartMealPlanner.Application.Recipes.Queries.GetRecipeById;

public record GetRecipeByIdQuery(int Id) : IRequest<RecipeDetailDto>;
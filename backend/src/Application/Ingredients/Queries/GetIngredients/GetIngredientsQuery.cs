namespace SmartMealPlanner.Application.Ingredients.Queries.GetIngredients;

public record GetIngredientsQuery : IRequest<IEnumerable<IngredientDto>>;
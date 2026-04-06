namespace SmartMealPlanner.Application.Ingredients.Queries.GetIngredientById;

public record GetIngredientByIdQuery(int Id) : IRequest<IngredientDetailDto>;
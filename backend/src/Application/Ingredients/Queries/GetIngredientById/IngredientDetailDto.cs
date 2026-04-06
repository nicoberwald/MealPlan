// This is a Data Transfer Object that we return to the cclient.

namespace SmartMealPlanner.Application.Ingredients.Queries.GetIngredientById;

public class IngredientDetailDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public IngredientCategory Category { get; init; }

    // AutoMapper mapping (conversion from Ingredient to IngredientDetailDto)
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Ingredient, IngredientDetailDto>();
        }
    }
}
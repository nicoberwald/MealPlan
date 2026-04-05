// This is a Data Transfer Object that we return to the cclient.

namespace SmartMealPlanner.Application.Recipes.Queries.GetRecipeById;

public class RecipeDetailDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int Servings { get; init; }

    public int PrepTimeMinutes { get; init; }

    public int CookTimeMinutes { get; init; }

    public bool IsAiGenerated { get; init; }

    // AutoMapper mapping (conversion from Recipe to RecipeDetailDto)
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Recipe, RecipeDetailDto>();
        }
    }
}
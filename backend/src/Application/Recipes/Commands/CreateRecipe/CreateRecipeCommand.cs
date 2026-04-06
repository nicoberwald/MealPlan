namespace SmartMealPlanner.Application.Recipes.Commands.CreateRecipe;

public record CreateRecipeCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty; // Default er en tom string

    public string Description { get; init; } = string.Empty; // Default er en tom string

    public int Servings { get; init; }

    public int PrepTimeMinutes { get; init; }

    public int CookTimeMinutes { get; init; }

    public IList<CreateRecipeIngredientDto> Ingredients { get; init; } = new List<CreateRecipeIngredientDto>();
}

public record CreateRecipeIngredientDto                                                     
  {
      public string Name { get; init; } = string.Empty;
      public decimal Quantity { get; init; }
      public MeasurementUnit Unit { get; init; }                                                
  }
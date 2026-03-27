namespace SmartMealPlanner.Domain.Entities;

// Recipe class that inherits from BaseAuditableEntity
public class Recipe : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Servings { get; set; }

    public int PrepTimeMinutes { get; set; }

    public int CookTimeMinutes { get; set; }

    public bool IsAiGenerated { get; set; }

    // This is a navigation property
    public IList<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
}
namespace SmartMealPlanner.Domain.Entities;

// Ingredients class that inherits from BaseAuditableEntity and represents an ingredient in the meal planner application
public class Ingredient : BaseAuditableEntity
{
    // Name of the ingredient (Non-nullable)
    public string Name { get; set; } = string.Empty;

    // Category of the ingredient (e.g., Meat, Fish, Dairy, etc.)
    public IngredientCategory Category { get; set; }
}
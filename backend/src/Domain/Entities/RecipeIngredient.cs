namespace SmartMealPlanner.Domain.Entities;

public class RecipeIngredient : BaseAuditableEntity
{
    public int RecipeId { get; set; }

    // Navigation property. Will be fetched from the database
    public Recipe Recipe { get; set; } = null!;

    public int IngredientId { get; set; }

    // Navigation property. Will be fetched from the database
    public Ingredient Ingredient { get; set; } = null!;

    public decimal Quantity { get; set; }

    public MeasurementUnit Unit { get; set; }
}
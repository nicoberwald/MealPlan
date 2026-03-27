namespace SmartMealPlanner.Domain.Entities;

public class MealPlanEntry : BaseAuditableEntity
{
    public int MealPlanId { get; set; }

    public MealPlan MealPlan { get; set; } = null!;

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = null!;

    public WeekDay Day { get; set; }

    public MealType MealType { get; set; }
}
namespace SmartMealPlanner.Domain.Entities;

public class MealPlan : BaseAuditableEntity
{
    public DateOnly WeekStartDate { get; set; }

    public IList<MealPlanEntry> Entries { get; set; } = new List<MealPlanEntry>();
}
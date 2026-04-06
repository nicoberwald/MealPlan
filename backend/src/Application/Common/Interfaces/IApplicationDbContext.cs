namespace SmartMealPlanner.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<Recipe> Recipes { get; }
    DbSet<Ingredient> Ingredients { get; }
    DbSet<MealPlan> MealPlans { get; }
    DbSet<MealPlanEntry> MealPlanEntries { get; }
}

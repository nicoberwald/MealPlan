namespace SmartMealPlanner.Infrastructure.Data.Configurations;

public class MealPlanEntryConfiguration : IEntityTypeConfiguration<MealPlanEntry>
{
    public void Configure(EntityTypeBuilder<MealPlanEntry> builder)
    {
        builder.HasOne(e => e.MealPlan)
            .WithMany(m => m.Entries)
            .HasForeignKey(e => e.MealPlanId);

        builder.HasOne(e => e.Recipe)
            .WithMany()
            .HasForeignKey(e => e.RecipeId);
    }
}
namespace SmartMealPlanner.Infrastructure.Data.Configurations;

public class MealPlanConfiguration : IEntityTypeConfiguration<MealPlan>
{
    public void Configure(EntityTypeBuilder<MealPlan> builder)
    {
        builder.Property(t => t.WeekStartDate)
            .IsRequired();
    }
}
namespace SmartMealPlanner.Infrastructure.Data.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(c => c.Description)
            .HasMaxLength(2000)
            .IsRequired();
    }
}
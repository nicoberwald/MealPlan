namespace SmartMealPlanner.Application.MealPlans.Queries.GetMealPlanById;

public class MealPlanEntryDto
{
    public int Id { get; init; }
    public int RecipeId { get; init; }
    public string RecipeName { get; init; } = string.Empty;
    public WeekDay Day { get; init; }
    public MealType MealType { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MealPlanEntry, MealPlanEntryDto>();
        }
    }
}
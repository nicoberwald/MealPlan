namespace SmartMealPlanner.Application.MealPlans.Queries.GetMealPlans;

// This is a Data Transfer Object that we return to the cclient.

public class MealPlanDto
{
    public int Id { get; init; }

    public DateOnly WeekStartDate { get; init; }

    // AutoMapper mapping (conversion from MealPlan to MealPlanDto)
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MealPlan, MealPlanDto>();
        }
    }
}
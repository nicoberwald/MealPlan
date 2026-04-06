namespace SmartMealPlanner.Application.MealPlans.Queries.GetMealPlanById;
// This is a Data Transfer Object that we return to the cclient.

public class MealPlanDetailDto
{
    public int Id { get; init; }

    public DateOnly WeekStartDate { get; init; }

    public IList<MealPlanEntryDto> Entries { get; init; } = new List<MealPlanEntryDto>();

    // AutoMapper mapping (conversion from MealPlan to MealPlanDetailDto)
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MealPlan, MealPlanDetailDto>().ForMember(d => d.Entries, o => o.MapFrom(s => s.Entries));
        }
    }
}
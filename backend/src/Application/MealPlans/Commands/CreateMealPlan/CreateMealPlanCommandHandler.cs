namespace SmartMealPlanner.Application.MealPlans.Commands.CreateMealPlan;

// The class that does the job:
public class CreateMealPlanCommandHandler : IRequestHandler<CreateMealPlanCommand, int>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public CreateMealPlanCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Create the mealplan object:
    public async Task<int> Handle(CreateMealPlanCommand request, CancellationToken ct)
    {
        var entity = new MealPlan { WeekStartDate = request.WeekStartDate };
        _context.MealPlans.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
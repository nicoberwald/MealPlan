namespace SmartMealPlanner.Application.MealPlans.Commands.AddMealPlanEntry;
// The class that does the job:
public class AddMealPlanEntryCommandHandler : IRequestHandler<AddMealPlanEntryCommand, int>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public AddMealPlanEntryCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Create the mealplan entry object:
    public async Task<int> Handle(AddMealPlanEntryCommand request, CancellationToken ct)
    {
        var entity = new MealPlanEntry { MealPlanId = request.MealPlanId, RecipeId = request.RecipeId, Day = request.Day, MealType = request.MealType };
        _context.MealPlanEntries.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
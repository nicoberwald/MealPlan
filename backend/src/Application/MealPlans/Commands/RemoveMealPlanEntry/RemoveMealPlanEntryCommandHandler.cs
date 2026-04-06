namespace SmartMealPlanner.Application.MealPlans.Commands.RemoveMealPlanEntry;

// The class that does the job:
public class RemoveMealPlanEntryCommandHandler : IRequestHandler<RemoveMealPlanEntryCommand>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public RemoveMealPlanEntryCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Delete the mealplan entry object:
    public async Task Handle(RemoveMealPlanEntryCommand request, CancellationToken ct)
    {
        var entity = await _context.MealPlanEntries.FindAsync(new object[] { request.Id }, ct);
        Guard.Against.NotFound(request.Id, entity);
        _context.MealPlanEntries.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
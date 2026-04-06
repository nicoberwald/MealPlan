namespace SmartMealPlanner.Application.MealPlans.Commands.DeleteMealPlan;

// The class that does the job:
public class DeleteMealPlanCommandHandler : IRequestHandler<DeleteMealPlanCommand>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public DeleteMealPlanCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Delete the mealplan object:
    public async Task Handle(DeleteMealPlanCommand request, CancellationToken ct)
    {
        var entity = await _context.MealPlans.FindAsync(new object[] { request.Id }, ct);
        Guard.Against.NotFound(request.Id, entity);
        _context.MealPlans.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
namespace SmartMealPlanner.Application.Ingredients.Commands.UpdateIngredient;

// The class that does the job:
public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public UpdateIngredientCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Update the ingredient object:
    public async Task Handle(UpdateIngredientCommand request, CancellationToken ct)
    {
        var entity = await _context.Ingredients.FindAsync(new object[] { request.Id }, ct);
        Guard.Against.NotFound(request.Id, entity);
        // Update the logic:
        entity.Name = request.Name;
        entity.Category = request.Category;
        await _context.SaveChangesAsync(ct);
    }
}
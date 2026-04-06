namespace SmartMealPlanner.Application.Ingredients.Commands.DeleteIngredient;

// The class that does the job:
public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public DeleteIngredientCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Delete the Ingredient object:
    public async Task Handle(DeleteIngredientCommand request, CancellationToken ct)
    {
        var entity = await _context.Ingredients.FindAsync(new object[] { request.Id }, ct);
        Guard.Against.NotFound(request.Id, entity);
        _context.Ingredients.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
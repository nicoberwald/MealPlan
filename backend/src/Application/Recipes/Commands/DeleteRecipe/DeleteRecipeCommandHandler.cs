namespace SmartMealPlanner.Application.Recipes.Commands.DeleteRecipe;

// The class that does the job:
public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public DeleteRecipeCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Delete the recipe object:
    public async Task Handle(DeleteRecipeCommand request, CancellationToken ct)
    {
        var entity = await _context.Recipes.FindAsync(new object[] { request.Id }, ct);
        Guard.Against.NotFound(request.Id, entity);
        _context.Recipes.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
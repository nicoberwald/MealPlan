namespace SmartMealPlanner.Application.Recipes.Commands.UpdateRecipe;

// The class that does the job:
public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public UpdateRecipeCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Update the recipe object:
    public async Task Handle(UpdateRecipeCommand request, CancellationToken ct)
    {
        var entity = await _context.Recipes.FindAsync(new object[] { request.Id }, ct);
        Guard.Against.NotFound(request.Id, entity);
        // Update the logic:
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Servings = request.Servings;
        entity.PrepTimeMinutes = request.PrepTimeMinutes;
        entity.CookTimeMinutes = request.CookTimeMinutes;
        await _context.SaveChangesAsync(ct);
    }
}
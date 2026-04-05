namespace SmartMealPlanner.Application.Recipes.Commands.CreateRecipe;

// The class that does the job:
public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, int>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public CreateRecipeCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Create the recipe object:
    public async Task<int> Handle(CreateRecipeCommand request, CancellationToken ct)
    {
        var entity = new Recipe { Name = request.Name, Description = request.Description, Servings = request.Servings, PrepTimeMinutes = request.PrepTimeMinutes, CookTimeMinutes = request.CookTimeMinutes };
        _context.Recipes.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
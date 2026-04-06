namespace SmartMealPlanner.Application.Ingredients.Commands.CreateIngredient;

// The class that does the job:
public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, int>
{
    private readonly IApplicationDbContext _context;

    // Dependency injection:
    public CreateIngredientCommandHandler(IApplicationDbContext context)
        => _context = context;

    // Create the recipe object:
    public async Task<int> Handle(CreateIngredientCommand request, CancellationToken ct)
    {
        var entity = new Ingredient { Name = request.Name, Category = request.Category };
        _context.Ingredients.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
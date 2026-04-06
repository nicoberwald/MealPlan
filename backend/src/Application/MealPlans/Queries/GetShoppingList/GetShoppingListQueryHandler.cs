namespace SmartMealPlanner.Application.MealPlans.Queries.GetShoppingList;

public class GetShoppingListQueryHandler : IRequestHandler<GetShoppingListQuery,
IEnumerable<ShoppingListItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetShoppingListQueryHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<IEnumerable<ShoppingListItemDto>> Handle(GetShoppingListQuery request, CancellationToken ct)
    {
        var mealPlan = await _context.MealPlans
            .Include(m => m.Entries)
                .ThenInclude(e => e.Recipe)
                    .ThenInclude(r => r.Ingredients)
                        .ThenInclude(ri => ri.Ingredient)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == request.MealPlanId, ct);

        Guard.Against.NotFound(request.MealPlanId, mealPlan);

        return mealPlan.Entries
            .SelectMany(e => e.Recipe.Ingredients)
            .GroupBy(ri => (ri.Ingredient.Name, ri.Unit))
            .Select(g => new ShoppingListItemDto
            {
                Name = g.Key.Name,
                Quantity = g.Sum(ri => ri.Quantity),
                Unit = g.Key.Unit
            })
            .OrderBy(i => i.Name)
            .ToList();
    }
}
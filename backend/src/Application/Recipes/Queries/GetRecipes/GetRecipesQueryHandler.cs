
namespace SmartMealPlanner.Application.Recipes.Queries.GetRecipes;

public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, IEnumerable<RecipeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    // Dependency injection:
    public GetRecipesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecipeDto>> Handle(GetRecipesQuery request, CancellationToken ct)
    {
        return await _context.Recipes
            .AsNoTracking()
            .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider)
            .OrderBy(r => r.Name)
            .ToListAsync(ct);
    }
}
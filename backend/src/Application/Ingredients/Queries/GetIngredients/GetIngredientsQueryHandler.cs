
namespace SmartMealPlanner.Application.Ingredients.Queries.GetIngredients;

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, IEnumerable<IngredientDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    // Dependency injection:
    public GetIngredientsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IngredientDto>> Handle(GetIngredientsQuery request, CancellationToken ct)
    {
        return await _context.Ingredients
            .AsNoTracking()
            .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider)
            .OrderBy(r => r.Name)
            .ToListAsync(ct);
    }
}
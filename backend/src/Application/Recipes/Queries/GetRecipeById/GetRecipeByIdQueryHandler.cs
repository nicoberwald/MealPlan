namespace SmartMealPlanner.Application.Recipes.Queries.GetRecipeById;

public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDetailDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    // Dependency injection:
    public GetRecipeByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RecipeDetailDto> Handle(GetRecipeByIdQuery request, CancellationToken ct)
    {
        var entity = await _context.Recipes
        .AsNoTracking()
        .FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        Guard.Against.NotFound(request.Id, entity);

        return _mapper.Map<RecipeDetailDto>(entity);
    }
}
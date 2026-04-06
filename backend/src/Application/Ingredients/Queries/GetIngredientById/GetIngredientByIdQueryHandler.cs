namespace SmartMealPlanner.Application.Ingredients.Queries.GetIngredientById;

public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, IngredientDetailDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    // Dependency injection:
    public GetIngredientByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IngredientDetailDto> Handle(GetIngredientByIdQuery request, CancellationToken ct)
    {
        var entity = await _context.Ingredients
        .AsNoTracking()
        .FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        Guard.Against.NotFound(request.Id, entity);

        return _mapper.Map<IngredientDetailDto>(entity);
    }
}
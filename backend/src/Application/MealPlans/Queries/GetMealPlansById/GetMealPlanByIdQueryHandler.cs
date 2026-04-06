namespace SmartMealPlanner.Application.MealPlans.Queries.GetMealPlanById;

public class GetMealPlanByIdQueryHandler : IRequestHandler<GetMealPlanByIdQuery, MealPlanDetailDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    // Dependency injection:
    public GetMealPlanByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MealPlanDetailDto> Handle(GetMealPlanByIdQuery request, CancellationToken ct)
    {
        var entity = await _context.MealPlans
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.Id == request.Id, ct);

        Guard.Against.NotFound(request.Id, entity);

        return _mapper.Map<MealPlanDetailDto>(entity);
    }
}
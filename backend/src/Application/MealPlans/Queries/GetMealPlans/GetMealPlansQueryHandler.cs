namespace SmartMealPlanner.Application.MealPlans.Queries.GetMealPlans;

public class GetMealPlansQueryHandler : IRequestHandler<GetMealPlansQuery, IEnumerable<MealPlanDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    // Dependency injection:
    public GetMealPlansQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MealPlanDto>> Handle(GetMealPlansQuery request, CancellationToken ct)
    {
        return await _context.MealPlans
            .AsNoTracking()
            .ProjectTo<MealPlanDto>(_mapper.ConfigurationProvider)
            .OrderBy(m => m.WeekStartDate)
            .ToListAsync(ct);
    }
}
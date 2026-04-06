using System.Text.Json;
using SmartMealPlanner.Application.Recipes.Commands.CreateRecipe;

namespace SmartMealPlanner.Application.Recipes.Commands.GenerateRecipe;

public class GenerateRecipeCommandHandler : IRequestHandler<GenerateRecipeCommand, int>
{
    private readonly IGeminiService _geminiService;
    private readonly ISender _sender;

    public GenerateRecipeCommandHandler(IGeminiService geminiService, ISender sender)
    {
        _geminiService = geminiService;
        _sender = sender;
    }

    public async Task<int> Handle(GenerateRecipeCommand request, CancellationToken ct)
    {
        var json = await _geminiService.GenerateRecipeJsonAsync(request.Prompt, ct);
        var command = JsonSerializer.Deserialize<CreateRecipeCommand>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        });

        Guard.Against.Null(command, nameof(command));

        return await _sender.Send(command, ct);
    }
}
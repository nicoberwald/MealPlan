namespace SmartMealPlanner.Application.Common.Interfaces;

public interface IGeminiService
{
    Task<string> GenerateRecipeJsonAsync(string prompt, CancellationToken ct);
}
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SmartMealPlanner.Application.Common.Interfaces;

namespace SmartMealPlanner.Infrastructure.Services;

public class GeminiService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Gemini:ApiKey"]!;
    }

    public async Task<string> GenerateRecipeJsonAsync(string prompt, CancellationToken ct)
    {
        const string systemInstruction = "You are a recipe assistant. Always respond with a single valid JSON object in this exact format, no markdown, no explanation: {\"name\": \"string\", \"description\": \"string\", \"servings\": 0, \"prepTimeMinutes\": 0, \"cookTimeMinutes\": 0}";

        var request = new
        {
            system_instruction = new
            {
                parts = new[] { new { text = systemInstruction } }
            },
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}",
            request,
            ct);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);

        return result
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString()!;
    }
}

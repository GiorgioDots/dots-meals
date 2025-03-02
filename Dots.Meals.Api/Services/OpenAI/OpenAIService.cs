using Dots.Meals.Api.Extensions;
using Dots.Meals.Api.Services.OpenAI.Prompts;
using Dots.Meals.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using OpenAI.Chat;
using System.Reflection;
using System.Text.Json;

namespace Dots.Meals.Api.Services.OpenAI;

public class OpenAiService
{
    private readonly ChatClient _client;
    private readonly string Model = Envs.OpenAIModel;

    public OpenAiService(AppSettings settings)
    {
        string apiKey = settings.OpenAIKey;
        _client = new ChatClient(Model, apiKey);
    }

    public async Task<GenerateWeeklyMealPrompt.Result?> GenerateMealPlanAsync(DAL.Entities.User userData, CancellationToken c = default)
    {
        var prompt = new GenerateWeeklyMealPrompt(_client, userData);
        return await prompt.GetResult(c);
    }
}

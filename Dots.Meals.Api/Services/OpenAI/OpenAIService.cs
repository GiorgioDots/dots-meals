using Dots.Meals.Api.Extensions;
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

    public async Task<MealPlan?> GenerateMealPlanAsync(Users userData, CancellationToken c = default)
    {
        var formattedData = ConvertEnumsToDescriptions(userData);
        var userDataJson = JsonSerializer.Serialize(formattedData, new JsonSerializerOptions { WriteIndented = true });

        var prompt = $@"
            You are a professional nutritionist. Generate a **strictly valid JSON** response.
            The JSON must have the following structure:

            {{
                ""days"": [
                    {{
                        ""day"": ""Monday"",
                        ""meals"": [
                            {{ ""meal"": ""Breakfast"", ""food"": ""Oatmeal with banana and almond butter"" }},
                            {{ ""meal"": ""Lunch"", ""food"": ""Grilled tofu with quinoa and steamed vegetables"" }},
                            {{ ""meal"": ""Dinner"", ""food"": ""Lentil soup with whole grain bread"" }}
                        ]
                    }},
                    {{
                        ""day"": ""Tuesday"",
                        ""meals"": [
                            {{ ""meal"": ""Breakfast"", ""food"": ""Greek yogurt with honey and granola"" }},
                            {{ ""meal"": ""Lunch"", ""food"": ""Chickpea salad with avocado"" }},
                            {{ ""meal"": ""Dinner"", ""food"": ""Stir-fried vegetables with brown rice"" }}
                        ]
                    }}
                ]
            }}

            The response **MUST** be valid JSON, without extra text or explanations.
    
            Generate a 7-day meal plan for this user:
            {{userDataJson}}
        ";
        var completion = await _client.CompleteChatAsync(
            [
                new SystemChatMessage("You are a professional nutritionist. Always respond in JSON."),
                new UserChatMessage(prompt)
            ],
            new ChatCompletionOptions
            {
                MaxOutputTokenCount = 1000,
                Temperature = 0.7f
            },
            cancellationToken: c
        );
        var result = completion.Value.Content.FirstOrDefault()?.Text ?? "{}";

        if (!IsValidJson(result))
            return null;

        // Deserialize JSON into MealPlan object
        var mealPlan = JsonSerializer.Deserialize<MealPlan>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (mealPlan == null)
            return null;

        mealPlan.UserId = userData.Id;

        return mealPlan;
    }

    private object ConvertEnumsToDescriptions(object obj)
    {
        if (obj == null) return null;

        Type type = obj.GetType();
        var dictionary = new Dictionary<string, object>();

        foreach (PropertyInfo prop in type.GetProperties())
        {
            object value = prop.GetValue(obj);

            if (value == null)
            {
                dictionary[prop.Name] = null;
                continue;
            }

            // If it's an Enum, get its description
            if (value is Enum enumValue)
            {
                dictionary[prop.Name] = enumValue.GetDescription();
            }
            else
            {
                dictionary[prop.Name] = value;
            }
        }

        return dictionary;
    }

    private bool IsValidJson(string input)
    {
        try
        {
            JsonDocument.Parse(input);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

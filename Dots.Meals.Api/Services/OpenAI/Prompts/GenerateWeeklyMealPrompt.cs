using Dots.Meals.DAL.Entities;
using OpenAI.Chat;
using System.Text.Json;

namespace Dots.Meals.Api.Services.OpenAI.Prompts;

public class GenerateWeeklyMealPrompt : Prompt<GenerateWeeklyMealPrompt.Result>
{
    public GenerateWeeklyMealPrompt(ChatClient client, DAL.Entities.User user) : base(client)
    {
        var formattedData = ConvertEnumsToDescriptions(user);
        var userDataJson = JsonSerializer.Serialize(formattedData, new JsonSerializerOptions { WriteIndented = true });

        Prompts = [
            new SystemChatMessage("You are a professional nutritionist. Always respond in JSON."),
            new UserChatMessage($@"
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
            {{userDataJson}}")
        ];

        ChatCompletionOptions = new ChatCompletionOptions
        {
            MaxOutputTokenCount = 1000,
            Temperature = 0.7f
        };
    }

    public override Task<Result?> ParseTextResult(string? txt, CancellationToken c = default)
    {
        var result = txt ?? "{}";

        if (!IsValidJson(result))
            return Task.FromResult<Result?>(null);

        // Deserialize JSON into MealPlan object
        var mealPlan = JsonSerializer.Deserialize<Result>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (mealPlan == null)
            return Task.FromResult<Result?>(null);

        return Task.FromResult<Result?>(mealPlan);
    }

    public class Result
    {
        public List<Day> days { get; set; }

        public class Day
        {
            public string day { get; set; }
            public List<Meal> meals { get; set; }
        }

        public class Meal
        {
            public string meal { get; set; }
            public string food { get; set; }
        }
    }
}

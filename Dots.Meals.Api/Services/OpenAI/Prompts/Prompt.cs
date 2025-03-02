using Dots.Meals.Api.Extensions;
using OpenAI.Chat;
using System.Reflection;
using System.Text.Json;

namespace Dots.Meals.Api.Services.OpenAI.Prompts;

public abstract class Prompt<TResult>
{
    public List<ChatMessage> Prompts { get; protected set; } = [];
    public ChatCompletionOptions? ChatCompletionOptions = null;
    private readonly ChatClient client;

    protected Prompt(ChatClient client)
    {
        this.client = client;
    }

    public async Task<TResult?> GetResult(CancellationToken c = default)
    {
        if (Prompts.Count == 0)
            throw new Exception("No prompts set");
        var completion = await client.CompleteChatAsync(Prompts, ChatCompletionOptions, cancellationToken: c);

        var textResult = completion.Value.Content.FirstOrDefault()?.Text;

        #region TestData
        //        var textResult = @"{
        //    ""days"": [
        //        {
        //            ""day"": ""Monday"",
        //            ""meals"": [
        //                { ""meal"": ""Breakfast"", ""food"": ""Oatmeal with banana and almond butter"" },
        //                { ""meal"": ""Lunch"", ""food"": ""Grilled tofu with quinoa and steamed vegetables"" },
        //                { ""meal"": ""Dinner"", ""food"": ""Lentil soup with whole grain bread"" }
        //            ]
        //        },
        //        {
        //            ""day"": ""Tuesday"",
        //            ""meals"": [
        //                { ""meal"": ""Breakfast"", ""food"": ""Greek yogurt with honey and granola"" },
        //                { ""meal"": ""Lunch"", ""food"": ""Chickpea salad with avocado"" },
        //                { ""meal"": ""Dinner"", ""food"": ""Stir-fried vegetables with brown rice"" }
        //            ]
        //        },
        //        {
        //            ""day"": ""Wednesday"",
        //            ""meals"": [
        //                { ""meal"": ""Breakfast"", ""food"": ""Scrambled eggs with spinach and whole grain toast"" },
        //                { ""meal"": ""Lunch"", ""food"": ""Turkey sandwich with lettuce and tomato"" },
        //                { ""meal"": ""Dinner"", ""food"": ""Baked salmon with quinoa and roasted asparagus"" }
        //            ]
        //        },
        //        {
        //            ""day"": ""Thursday"",
        //            ""meals"": [
        //                { ""meal"": ""Breakfast"", ""food"": ""Chia seed pudding with berries"" },
        //                { ""meal"": ""Lunch"", ""food"": ""Vegetable stir-fry with tofu"" },
        //                { ""meal"": ""Dinner"", ""food"": ""Black bean tacos with avocado salsa"" }
        //            ]
        //        },
        //        {
        //            ""day"": ""Friday"",
        //            ""meals"": [
        //                { ""meal"": ""Breakfast"", ""food"": ""Smoothie bowl with mixed fruits and nuts"" },
        //                { ""meal"": ""Lunch"", ""food"": ""Quinoa salad with roasted vegetables"" },
        //                { ""meal"": ""Dinner"", ""food"": ""Vegetable curry with brown rice"" }
        //            ]
        //        },
        //        {
        //            ""day"": ""Saturday"",
        //            ""meals"": [
        //                { ""meal"": ""Breakfast"", ""food"": ""Whole grain pancakes with maple syrup and berries"" },
        //                { ""meal"": ""Lunch"", ""food"": ""Mediterranean salad with feta cheese"" },
        //                { ""meal"": ""Dinner"", ""food"": ""Grilled chicken with sweet potato and green beans"" }
        //            ]
        //        },
        //        {
        //            ""day"": ""Sunday"",
        //            ""meals"": [
        //                { ""meal"": ""Breakfast"", ""food"": ""Avocado toast with poached eggs"" },
        //                { ""meal"": ""Lunch"", ""food"": ""Cauliflower soup with crusty bread"" },
        //                { ""meal"": ""Dinner"", ""food"": ""Spaghetti squash with marinara sauce and turkey meatballs"" }
        //            ]
        //        }
        //    ]
        //}";
        #endregion

        return await ParseTextResult(textResult, c);
    }

    public abstract Task<TResult?> ParseTextResult(string? txt, CancellationToken c = default);


    protected object ConvertEnumsToDescriptions(object obj)
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

    protected bool IsValidJson(string input)
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

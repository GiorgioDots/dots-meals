using Dots.Meals.Api.Extensions;
using OpenAI.Chat;
using System.Reflection;
using System.Text.Json;

namespace Dots.Meals.Api.Services.OpenAI.Prompts;

internal abstract class Prompt<TResult>
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
        var completion = await client.CompleteChatAsync(Prompts, ChatCompletionOptions,cancellationToken: c);

        var textResult = completion.Value.Content.FirstOrDefault()?.Text;

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

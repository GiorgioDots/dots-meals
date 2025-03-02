namespace Dots.Meals.Api;

public class AppSettings
{
    public string DbUrl { get; private set; }
    public string JwtSecret { get; private set; }
    public string OpenAIKey { get; private set; }

    public AppSettings()
    {
        // Reading secrets from environment variables pointing to the files
        DbUrl = ReadSecretFile(Environment.GetEnvironmentVariable("DB_URL_FILE"));
        JwtSecret = ReadSecretFile(Environment.GetEnvironmentVariable("JWT_SECRET_FILE"));
        OpenAIKey = ReadSecretFile(Environment.GetEnvironmentVariable("OPENAI_KEY_FILE"));
    }

    private string ReadSecretFile(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("Secret file path is not defined.");
        }

        return File.Exists(filePath)
            ? File.ReadAllText(filePath)
            : throw new FileNotFoundException($"Secret file not found: {filePath}");
    }
}

namespace Dots.Meals.Api;

public static class Envs
{
    public static string JwtIssuer => EnsureEnv("JWT_ISSUER");
    public static string JwtAudience => EnsureEnv("JWT_AUDIENCE");
    public static string AllowedOrigins => EnsureEnv("ALLOWED_ORIGINS");
    public static string OpenAIModel = EnsureEnv("OPENAI_MODEL");

    private static string EnsureEnv(string env)
    {
        return Environment.GetEnvironmentVariable(env) 
            ?? throw new ArgumentNullException($"Environment {env} not found");
    }
}

namespace Enums.RetrieveTranslations;

internal sealed class EnumOptions
{
    public required string EnumName { get; set; }
    public required List<EnumOption> Options { get; set; }

}

internal sealed class EnumOption
{
    public required string Key { get; set; }
    public required string Description { get; set; }
    public required int Value { get; set; }
}


using homework_5.Configurations.Sources;
using homework_5.Parsers;

namespace homework_5.Configurations.Providers;

public class YamlConfigurationProvider : FileConfigurationProvider
{
    private readonly YamlConfigurationFileParser _parser = new();
    
    public YamlConfigurationProvider(YamlConfigurationSource source) : base(source) { }
    
    public override void Load(Stream stream)
    {
        try
        {
            Data = _parser.Parse(stream, Source.Path);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException(
                $"Не удалось загрузить YAML-конфигурацию из '{Source.Path ?? "потока"}': {ex.Message}",
                ex);
        }
    }
}
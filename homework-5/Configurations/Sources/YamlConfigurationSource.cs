using homework_5.Configurations.Providers;

namespace homework_5.Configurations.Sources;

public class YamlConfigurationSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        FileProvider ??= builder.GetFileProvider();
        return new YamlConfigurationProvider(this);
    }
}
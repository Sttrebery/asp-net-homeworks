using homework_5.Configurations.Sources;

namespace homework_5.Configurations;

public static class ConfigurationBuilderExtensions
{
    public static void AddYamlFile(this IConfigurationBuilder builder, string path, bool optional = false)
    {
        if (string.IsNullOrEmpty(path.Trim()))
            throw new ArgumentException("File path cannot be null or empty.", nameof(path));
        
        builder.AddYamlFile(source =>
        {
            source.FileProvider = null;
            source.Path =  path;
            source.Optional = optional;
        });
    }
    
    public static void AddYamlFile(this IConfigurationBuilder builder, Action<YamlConfigurationSource> source)
    {
        builder.Add(source);
    }
}
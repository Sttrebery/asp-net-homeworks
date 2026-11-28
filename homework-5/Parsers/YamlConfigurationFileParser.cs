using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace homework_5.Parsers;

internal class YamlConfigurationFileParser
{
    public IDictionary<string, string?> Parse(Stream input, string? sourceName = null)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        var data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        using var reader = new StreamReader(input, leaveOpen: true);
        var yaml = new YamlStream();
        yaml.Load(reader);

        if (yaml.Documents.Count > 0)
        {
            VisitNode(yaml.Documents[0].RootNode, currentPath: null, data);
        }

        return data;
    }

    private static void VisitNode(
        YamlNode node,
        string? currentPath,
        IDictionary<string, string?> data)
    {
        switch (node)
        {
            case YamlMappingNode mapping:
            {
                foreach (var entry in mapping.Children)
                {
                    if (entry.Key is not YamlScalarNode keyScalar || keyScalar.Value is null)
                        continue;

                    var nextPath = string.IsNullOrEmpty(currentPath)
                        ? keyScalar.Value
                        : ConfigurationPath.Combine(currentPath, keyScalar.Value);

                    VisitNode(entry.Value, nextPath, data);
                }
                break;
            }

            case YamlSequenceNode sequence:
            {
                for (var i = 0; i < sequence.Children.Count; i++)
                {
                    var nextPath = ConfigurationPath.Combine(currentPath ?? string.Empty, i.ToString());
                    VisitNode(sequence.Children[i], nextPath, data);
                }
                break;
            }

            case YamlScalarNode scalar:
            {
                if (!string.IsNullOrEmpty(currentPath))
                {
                    data[currentPath] = scalar.Value;
                }
                break;
            }
        }
    }
}
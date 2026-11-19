using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace homework_2.Shapes;

public interface IShapeStorage
{
    void Save(string filePath, GeometricShape[] shapes);
    GeometricShape[] Load(string filePath);
}

public class JsonShapeStorage : IShapeStorage
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
    };

    public void Save(string filePath, GeometricShape[] shapes)
    {
        ArgumentNullException.ThrowIfNull(shapes);
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(shapes, Options);
        File.WriteAllText(filePath, json);
    }

    public GeometricShape[] Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<GeometricShape[]>(json, Options) ?? [];
    }
}

public class TextShapeStorage : IShapeStorage
{
    public void Save(string filePath, GeometricShape[] shapes)
    {
        ArgumentNullException.ThrowIfNull(shapes);
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var lines = new List<string>(shapes.Length);
        foreach (var shape in shapes)
        {
            string line = shape switch
            {
                RectangleShape rect => $"RECT|{rect.FigureName}|{rect.Width}|{rect.Height}",
                TriangleShape tri => $"TRI|{tri.FigureName}|{tri.Height}",
                CircleShape circ => $"CIRC|{circ.FigureName}|{circ.Radius}",
                _ => throw new NotSupportedException($"Тип фигуры {shape.GetType().Name} не поддерживается для TXT.")
            };
            lines.Add(line);
        }

        File.WriteAllLines(filePath, lines);
    }

    public GeometricShape[] Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        var result = new List<GeometricShape>();
        foreach (var rawLine in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(rawLine))
            {
                continue;
            }

            var parts = rawLine.Split('|');
            string kind = parts[0];
            string name = parts[1];

            GeometricShape shape = kind switch
            {
                "RECT" => new RectangleShape
                {
                    FigureName = name,
                    Width = int.Parse(parts[2], CultureInfo.InvariantCulture),
                    Height = int.Parse(parts[3], CultureInfo.InvariantCulture)
                },
                "TRI" => new TriangleShape
                {
                    FigureName = name,
                    Height = int.Parse(parts[2], CultureInfo.InvariantCulture)
                },
                "CIRC" => new CircleShape
                {
                    FigureName = name,
                    Radius = int.Parse(parts[2], CultureInfo.InvariantCulture)
                },
                _ => throw new FormatException($"Неизвестный тип фигуры в файле: {kind}")
            };

            result.Add(shape);
        }

        return result.ToArray();
    }
}

public static class ShapeStorageFactory
{
    public static IShapeStorage CreateByExtension(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".json" => new JsonShapeStorage(),
            ".txt" => new TextShapeStorage(),
            _ => throw new NotSupportedException($"Формат '{ext}' не поддерживается. Используйте .json или .txt.")
        };
    }
}
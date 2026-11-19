using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace homework_2.Animals;

public interface IAnimalStorage
{
    void Save(string filePath, Animal[] animals);
    Animal[] Load(string filePath);
}

public class JsonAnimalStorage : IAnimalStorage
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
    };

    public void Save(string filePath, Animal[] animals)
    {
        ArgumentNullException.ThrowIfNull(animals);
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(animals, Options);
        File.WriteAllText(filePath, json);
    }

    public Animal[] Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Animal[]>(json, Options) ?? [];
    }
}

public class TextAnimalStorage : IAnimalStorage
{
    public void Save(string filePath, Animal[] animals)
    {
        ArgumentNullException.ThrowIfNull(animals);
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var lines = new List<string>(animals.Length);
        foreach (var animal in animals)
        {
            string line = animal switch
            {
                Dog dog => $"DOG|{dog.Name}|{dog.Age}|{dog.Breed}",
                Cat cat => $"CAT|{cat.Name}|{cat.Age}|{cat.IsIndoor}",
                Cow cow => $"COW|{cow.Name}|{cow.Age}|{cow.DailyMilkLiters.ToString(CultureInfo.InvariantCulture)}",
                _ => throw new NotSupportedException($"Тип животного {animal.GetType().Name} не поддерживается для TXT.")
            };
            lines.Add(line);
        }

        File.WriteAllLines(filePath, lines);
    }

    public Animal[] Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        var result = new List<Animal>();
        foreach (var rawLine in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(rawLine))
            {
                continue;
            }

            var parts = rawLine.Split('|');
            string kind = parts[0];
            string name = parts[1];
            int age = int.Parse(parts[2], CultureInfo.InvariantCulture);

            Animal animal = kind switch
            {
                "DOG" => new Dog
                {
                    Name = name,
                    Age = age,
                    Breed = parts.Length > 3 ? parts[3] : string.Empty
                },
                "CAT" => new Cat
                {
                    Name = name,
                    Age = age,
                    IsIndoor = parts.Length > 3 && bool.Parse(parts[3])
                },
                "COW" => new Cow
                {
                    Name = name,
                    Age = age,
                    DailyMilkLiters = parts.Length > 3
                        ? double.Parse(parts[3], CultureInfo.InvariantCulture)
                        : 0.0
                },
                _ => throw new FormatException($"Неизвестный тип животного в файле: {kind}")
            };

            result.Add(animal);
        }

        return result.ToArray();
    }
}

public static class AnimalStorageFactory
{
    public static IAnimalStorage CreateByExtension(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".json" => new JsonAnimalStorage(),
            ".txt" => new TextAnimalStorage(),
            _ => throw new NotSupportedException($"Формат '{ext}' не поддерживается. Используйте .json или .txt.")
        };
    }
}
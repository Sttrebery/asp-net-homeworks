using System.Text.Json.Serialization;
using homework_2.Output;

namespace homework_2.Animals;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Dog), "dog")]
[JsonDerivedType(typeof(Cat), "cat")]
[JsonDerivedType(typeof(Cow), "cow")]
public abstract class Animal
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public abstract string Sound { get; }

    public void ShowName(IOutputPrint output)
    {
        ArgumentNullException.ThrowIfNull(output);
        output.WriteLine($"Животное: {Name}");
    }

    public void ShowSound(IOutputPrint output)
    {
        ArgumentNullException.ThrowIfNull(output);
        output.WriteLine($"Звук ({Name}): {Sound}");
    }

    public override string ToString() => $"{GetType().Name} [Name={Name}, Age={Age}, Sound={Sound}]";
}

public sealed class Dog : Animal
{
    public string Breed { get; set; } = string.Empty;
    public override string Sound => "Гав-гав!";

    public override string ToString() =>
        $"Собака '{Name}' ({Breed}, {Age} лет) — звук: {Sound}";
}

public sealed class Cat : Animal
{
    public bool IsIndoor { get; set; } = true;
    public override string Sound => "Мяу!";

    public override string ToString() =>
        $"Кошка '{Name}' (домашняя: {(IsIndoor ? "да" : "нет")}, {Age} лет) — звук: {Sound}";
}

public sealed class Cow : Animal
{
    public double DailyMilkLiters { get; set; }
    public override string Sound => "Му-у-у!";

    public override string ToString() =>
        $"Корова '{Name}' (молока в день: {DailyMilkLiters:F1} л, {Age} лет) — звук: {Sound}";
}
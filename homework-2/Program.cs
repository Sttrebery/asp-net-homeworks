using homework_2.Animals;
using homework_2.Output;
using homework_2.Shapes;

Console.OutputEncoding = System.Text.Encoding.UTF8;

string outputDir = Path.Combine(AppContext.BaseDirectory, "output");
Directory.CreateDirectory(outputDir);

string animalsOutputLog = Path.Combine(outputDir, "animals_display.log");
string shapesOutputLog = Path.Combine(outputDir, "shapes_display.log");

if (File.Exists(animalsOutputLog)) File.Delete(animalsOutputLog);
if (File.Exists(shapesOutputLog)) File.Delete(shapesOutputLog);

var consoleTarget = new ConsoleOutputPrint();
var animalsFileTarget = new FileOutputPrint(animalsOutputLog);
var shapesFileTarget = new FileOutputPrint(shapesOutputLog);

// ЗАДАНИЕ 1
Console.WriteLine(" ## Отображение животных  ##");
Animal[] animals =
[
    new Dog { Name = "Рекс", Age = 4, Breed = "Овчарка" },
    new Cat { Name = "Мурка", Age = 2, IsIndoor = true },
    new Cow { Name = "Зорька", Age = 5, DailyMilkLiters = 18.5 }
];

foreach (var animal in animals)
{
    animal.ShowName(consoleTarget);
    animal.ShowSound(consoleTarget);

    animal.ShowName(animalsFileTarget);
    animal.ShowSound(animalsFileTarget);
}

// ЗАДАНИЕ 2
Console.WriteLine(" ## Сохранение и загрузка животных ##");
string animalsJsonPath = Path.Combine(outputDir, "animals.json");
string animalsTxtPath = Path.Combine(outputDir, "animals.txt");

AnimalStorageFactory.CreateByExtension(animalsJsonPath).Save(animalsJsonPath, animals);
AnimalStorageFactory.CreateByExtension(animalsTxtPath).Save(animalsTxtPath, animals);

Animal[] loadedFromJson = AnimalStorageFactory.CreateByExtension(animalsJsonPath).Load(animalsJsonPath);
Animal[] loadedFromTxt = AnimalStorageFactory.CreateByExtension(animalsTxtPath).Load(animalsTxtPath);

Console.WriteLine($"Загружено из JSON ({loadedFromJson.Length}):");
foreach (var a in loadedFromJson)
{
    Console.WriteLine($"  - {a}");
}

Console.WriteLine($"Загружено из TXT ({loadedFromTxt.Length}):");
foreach (var a in loadedFromTxt)
{
    Console.WriteLine($"  - {a}");
}

// ЗАДАНИЕ 3
Console.WriteLine(" ## Отображение фигур ##");
GeometricShape[] shapes =
[
    new RectangleShape { FigureName = "Прямоугольник", Width = 10, Height = 4 },
    new TriangleShape { FigureName = "Треугольник", Height = 5 },
    new CircleShape { FigureName = "Круг", Radius = 5 }
];

foreach (var shape in shapes)
{
    shape.ShowName(consoleTarget);
    shape.ShowVisual(consoleTarget);
    Console.WriteLine();

    shape.ShowName(shapesFileTarget);
    shape.ShowVisual(shapesFileTarget);
    shapesFileTarget.WriteLine(new string('-', 30));
}

// ЗАДАНИЕ 4
Console.WriteLine(" ## Сохранение и выгрузка фигур ##");
string shapesJsonPath = Path.Combine(outputDir, "shapes.json");
string shapesTxtPath = Path.Combine(outputDir, "shapes.txt");

ShapeStorageFactory.CreateByExtension(shapesJsonPath).Save(shapesJsonPath, shapes);
ShapeStorageFactory.CreateByExtension(shapesTxtPath).Save(shapesTxtPath, shapes);

GeometricShape[] loadedShapesJson = ShapeStorageFactory.CreateByExtension(shapesJsonPath).Load(shapesJsonPath);
GeometricShape[] loadedShapesTxt = ShapeStorageFactory.CreateByExtension(shapesTxtPath).Load(shapesTxtPath);

Console.WriteLine($"Загружено фигур из JSON: {loadedShapesJson.Length}");
foreach (var s in loadedShapesJson)
{
    s.ShowName(consoleTarget);
    s.ShowVisual(consoleTarget);
    Console.WriteLine();
}

Console.WriteLine($"Загружено фигур из TXT: {loadedShapesTxt.Length}");
foreach (var s in loadedShapesTxt)
{
    s.ShowName(consoleTarget);
    s.ShowVisual(consoleTarget);
    Console.WriteLine();
}

Console.WriteLine($"Файлы сохранены в: {outputDir}");
using System.Text;
using System.Text.Json.Serialization;
using homework_2.Output;

namespace homework_2.Shapes;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(RectangleShape), "rectangle")]
[JsonDerivedType(typeof(TriangleShape), "triangle")]
[JsonDerivedType(typeof(CircleShape), "circle")]
public abstract class GeometricShape
{
    public string FigureName { get; set; } = string.Empty;

    public abstract string Draw();

    public void ShowName(IOutputPrint output)
    {
        ArgumentNullException.ThrowIfNull(output);
        output.WriteLine($"Фигура: {FigureName}");
    }

    public void ShowVisual(IOutputPrint output)
    {
        ArgumentNullException.ThrowIfNull(output);
        output.WriteLine($"Визуальное представление ({FigureName}):");
        output.WriteLine(Draw());
    }
}

public sealed class RectangleShape : GeometricShape
{
    public int Width { get; set; }
    public int Height { get; set; }

    public override string Draw()
    {
        int w = Math.Max(2, Width);
        int h = Math.Max(2, Height);
        var sb = new StringBuilder();

        for (int row = 0; row < h; row++)
        {
            if (row == 0 || row == h - 1)
            {
                sb.AppendLine(new string('#', w));
            }
            else if (w == 2)
            {
                sb.AppendLine("##");
            }
            else
            {
                sb.Append('#');
                sb.Append(new string(' ', w - 2));
                sb.AppendLine("#");
            }
        }

        return sb.ToString().TrimEnd();
    }
}

public sealed class TriangleShape : GeometricShape
{
    public int Height { get; set; }

    public override string Draw()
    {
        int h = Math.Max(2, Height);
        var sb = new StringBuilder();

        for (int row = 1; row <= h; row++)
        {
            int leadingSpaces = h - row;
            sb.Append(new string(' ', leadingSpaces));

            if (row == 1)
            {
                sb.AppendLine("*");
            }
            else if (row == h)
            {
                sb.AppendLine(new string('*', 2 * h - 1));
            }
            else
            {
                sb.Append('*');
                sb.Append(new string(' ', 2 * (row - 1) - 1));
                sb.AppendLine("*");
            }
        }

        return sb.ToString().TrimEnd();
    }
}

public sealed class CircleShape : GeometricShape
{
    public int Radius { get; set; }

    public override string Draw()
    {
        int r = Math.Max(2, Radius);
        var sb = new StringBuilder();

        for (int y = -r; y <= r; y++)
        {
            for (int x = -r; x <= r; x++)
            {
                double dist = Math.Sqrt(x * x + y * y);
                if (Math.Abs(dist - r) < 0.55)
                {
                    sb.Append('o');
                }
                else
                {
                    sb.Append(' ');
                }
            }
            sb.AppendLine();
        }

        return sb.ToString().TrimEnd();
    }
}
namespace homework_2.Output;

public class FileOutputPrint (string filePath, bool append = true) : IOutputPrint
{
    public void WriteLine(string text)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using var writer = new StreamWriter(filePath, append);
        writer.WriteLine(text);
    }
}
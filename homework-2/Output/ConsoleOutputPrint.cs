namespace homework_2.Output;

public class ConsoleOutputPrint : IOutputPrint
{
    public void WriteLine(string text) => Console.WriteLine(text);
}
namespace homework_1.Models;

public class Country
{
    public string Name { get; set; }
    public string Capital { get; set; }
    public decimal Population { get; set; }
    public string[] Cities { get; set; }
    public string? Description { get; set; }
}
using homework_1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace homework_1.Pages;

public class RestarauntModel : PageModel
{
    public Restaraunt[] Restaraunts { get; private set; } = null!;
    
    public Restaraunt? SelectedRestaraunt { get; set; }
   
    public void OnGet()
    {
        Restaraunts = new[]
        {
            new Restaraunt() { Name = "Buono", Address = "Russia, Moscow", Rating = 4.7M },
            new Restaraunt() { Name = "Three Broomsticks", Address = "England, Hogsmid", Rating = 5.0M },
            new Restaraunt() { Name = "Loona", Address = "Italy, Milano", Rating = 3.9M },
            new Restaraunt() { Name = "Butler", Address = "German, Munchen", Rating = 2.3M }
        };
        
        SelectedRestaraunt = Restaraunts.FirstOrDefault();
    }
}
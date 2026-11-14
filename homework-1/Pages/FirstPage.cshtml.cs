using Microsoft.AspNetCore.Mvc.RazorPages;

namespace homework_1.Pages;

public class FirstPageModel : PageModel
{
    public int Number { get; set; }
    public char Letter { get; set; }
    
    public void OnGet()
    {
        Number = Random.Shared.Next(1, 27); // в английском 26 букв
        Letter = Convert.ToChar('A' + Number - 1);
    }
}
using homework_1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace homework_1.Pages;

public class CountriesModel : PageModel
{
    public Country[] Countries { get; private set; }
    
    public void OnGet()
    {
        Countries = new[]
        {
            new Country() 
            { 
                Name = "Исландия", 
                Capital = "Рейкьявик", 
                Population = 383.5M,
                Description = "Островное государство на севере Европы с уникальной природой и вулканами.",
                Cities = ["Рейкьявик", "Акюрейри", "Кеблавик", "Хабнарфьёдюр"]
            },
            new Country() 
            { 
                Name = "Черногория", 
                Capital = "Подгорица", 
                Population = 620.0M, 
                Description = "Живописная балканская страна с горами, каньонами и побережьем Адриатического моря.",
                Cities = ["Подгорица", "Никшич", "Котор", "Будва"]
            },
            new Country() 
            { 
                Name = "Люксембург", 
                Capital = "Люксембург", 
                Population = 660.8M, 
                Description = "Небольшое европейское государство с высоким уровнем жизни и средневековой архитектурой.",
                Cities = ["Люксембург", "Эш-сюр-Альзетт", "Дифферданж", "Дюделанж"]
            },
            new Country() 
            { 
                Name = "Мальта", 
                Capital = "Валлетта", 
                Population = 533.0M,
                Description = "Островное государство в Средиземном море с богатой историей и мягким климатом.",
                Cities = ["Валлетта", "Биркиркара", "Слима", "Моста"]
            },
            new Country() 
            { 
                Name = "Эстония", 
                Capital = "Таллин", 
                Population = 1320.0M, 
                Description = "Северная страна с развитыми цифровыми технологиями и побережьем Балтийского моря.",
                Cities = ["Таллин", "Тарту", "Нарва", "Пярну"]
            }
        };
    }
}
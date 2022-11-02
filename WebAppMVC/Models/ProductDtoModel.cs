using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppMVC.Models
{
    public class ProductDtoModel : PageModel
    {
        [BindProperty] // to write data (cant put to html)
        public string Name { get; set; }

        public int Id { get; set; }


        public void OnGet()
        {

        }
    }

    
}
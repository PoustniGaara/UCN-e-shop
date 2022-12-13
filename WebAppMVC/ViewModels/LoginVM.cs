using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.ViewModels
{
    public class LoginVM
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "*Fill out email field")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "*Fill out password field")]
        public string? Password { get; set; }

        [ViewData]
        public string? ErrorMessage { get; set; }

        [ViewData]
        public string? Message { get; set; }

    }
}

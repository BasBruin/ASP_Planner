using System.ComponentModel.DataAnnotations;

namespace Planner_ASP.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vul dit veld in!")]
        public string Gebruikersnaam { get; set; }
        [Required(ErrorMessage = "Vul dit veld in!")]
        public string Wachtwoord { get; set; }
    }
}

using blog_rpg.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Areas.Identity.Models;

// Add profile data for application users by adding properties to the IdentityUser class
public class ApplicationUser : IdentityUser
{
    private DateTime _birthDate;

    public ApplicationUser() : base()
    {
    }

    [Required]
    public Country Country { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate 
    {
        get => _birthDate;
        set
        {
            _birthDate = value;
            UpdateAge();
        }
    }

    public int Age { get; private set; }

    private void UpdateAge()
    {
        if (BirthDate.Ticks == 0)
            throw new InvalidOperationException("A data de nascimento do usuário não foi definida corretamente. Impossível calcular a idade.");
        int age = DateTime.Today.Year - BirthDate.Year;
        Age = DateTime.Today < BirthDate.AddYears(age) ? age-- : age;
    }
}


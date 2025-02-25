using System.ComponentModel.DataAnnotations;

namespace blog_rpg.CustomAttributes
{
    public class BirthDateValidatorAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate = new(1800, 1, 1);
        private readonly DateTime _maxDate;

        public BirthDateValidatorAttribute()
        {
            _maxDate = DateTime.Today;
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime birthDate)
            {
                return (birthDate >= _minDate) && (birthDate < _maxDate);
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} inválida."; 
        }
    }
}

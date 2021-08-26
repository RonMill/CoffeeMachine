using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KaffeemaschineWPF.ValidationRules
{
    public class OneSpaceRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return new ValidationResult(false, "Bitte E-Mail Adresse eintragen ");

            if (value.ToString().Contains(' '))
                return new ValidationResult(true, null);
            return new ValidationResult(false, "Bitte Straße und Hausnummer eintragen");
        }
    }
}
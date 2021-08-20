using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KaffeemaschineWPF.ValidationRules
{
    public class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(string.IsNullOrEmpty(value?.ToString()))
                return new ValidationResult(false, "Bitte E-Mail Adresse eintragen ");

            try
            {
                MailAddress m = new MailAddress(value.ToString());
                return new ValidationResult(true, null);
            }
            catch (FormatException e)
            {
                return new ValidationResult(false, e.Message);
            }
        }
    }
}

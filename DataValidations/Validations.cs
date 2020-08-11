using System;
using System.Text.RegularExpressions;

namespace DataValidations
{
    public class Validations
    {
        public bool IsDigitsOnly(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public bool IsValidPasswordFormat(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Password should not be empty");
            }
            return new Regex(@"^ (?=.*[a - z])(?=.*[A - Z])(?=.*\d)(?=.*[^\da - zA - Z]).{ 8}$").IsMatch(password);
        }
    }
}

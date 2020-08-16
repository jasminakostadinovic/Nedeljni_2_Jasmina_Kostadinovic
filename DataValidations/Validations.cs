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
              return false;
            }
            return new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,60}$").IsMatch(password);
        }

        public bool IsValidIDCardFormat(string idcardNo)
        {
            if (!IsDigitsOnly(idcardNo) || idcardNo.Length != 9)
                return false;
            return true;
        }
    }
}

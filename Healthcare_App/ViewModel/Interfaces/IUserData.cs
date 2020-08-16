using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare_App.ViewModel.Interfaces
{
    interface IUserData
    {
        bool CanSave { get; set; }
        string DateOfBirth { get; set; }
        string[] SexTypes { get; set; }
        string Password { get; set; }
        string Username { get; set; }
        string Citizenship { get; set; }
        string Sex { get; set; }
        string IDCardNo { get; set; }
        string Surname { get; set; }
        string GivenName { get; set; }
        DateTime DateDateValue { get; set; }
    }
}

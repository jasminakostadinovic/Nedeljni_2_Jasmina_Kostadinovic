using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareData.Models
{
    public partial class tblClinicManager
    {
        public string GivenName
        {
            get
            {
                return tblHealthcareUserData.GivenName;
            }
        }
        public string Surname
        {
            get
            {
                return tblHealthcareUserData.Surname;
            }
        }
        public string Sex
        {
            get
            {
                return tblHealthcareUserData.Sex;
            }
        }
        public string Citizenship
        {
            get
            {
                return tblHealthcareUserData.Citizenship;
            }
        }
        public string DateOfBirth
        {
            get
            {
                return tblHealthcareUserData.DateOfBirth.ToShortDateString();
            }
        }
    }
}

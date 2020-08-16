namespace HealthcareData.Models
{
    public partial class tblClinicDoctor
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

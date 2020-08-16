using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareData.Models
{
    public partial class tblHealthcareInstitution
    {
        public string DateOfBirth
        {
            get
            {
                return CompletionDate.ToShortDateString();
            }
        }
    }
}

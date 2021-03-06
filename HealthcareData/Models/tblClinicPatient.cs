//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthcareData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblClinicPatient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblClinicPatient()
        {
            this.tblSickLeaveRequirements = new HashSet<tblSickLeaveRequirement>();
        }
    
        public int ClinicPatientID { get; set; }
        public int UserDataID { get; set; }
        public Nullable<int> ClinicDoctorID { get; set; }
        public string HealthInsuranceCardNo { get; set; }
        public string NumberOfDoctor { get; set; }
    
        public virtual tblClinicDoctor tblClinicDoctor { get; set; }
        public virtual tblHealthcareUserData tblHealthcareUserData { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSickLeaveRequirement> tblSickLeaveRequirements { get; set; }
    }
}

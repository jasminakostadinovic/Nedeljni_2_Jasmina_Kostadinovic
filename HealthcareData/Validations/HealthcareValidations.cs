using DataValidations;
using HealthcareData.Models;
using System;
using System.Linq;

namespace HealthcareData.Validations
{
    public class HealthcareValidations
    {
        public bool IsCorrectUser(string userName, string password)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    var user = conn.tblHealthcareUserDatas.FirstOrDefault(x => x.Username == userName);

                    if (user != null)
                    {
                        var passwordFromDb = conn.tblHealthcareUserDatas.First(x => x.Username == userName).Password;
                        return SecurePasswordHasher.Verify(password, passwordFromDb);
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetUserType(int userDataId)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    if (conn.tblClinicDoctors.Any(x => x.UserDataID == userDataId))
                        return nameof(tblClinicDoctor);
                    if (conn.tblClinicMaintenances.Any(x => x.UserDataID == userDataId))
                        return nameof(tblClinicMaintenance);
                    if (conn.tblClinicManagers.Any(x => x.UserDataID == userDataId))
                        return nameof(tblClinicManager);
                    if (conn.tblClinicPatients.Any(x => x.UserDataID == userDataId))
                        return nameof(tblClinicPatient);
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsUniqueIDCardNo(string iDCardNo)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    return !conn.tblHealthcareUserDatas.Any(x => x.IDCardNo == iDCardNo);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsUniqueUsername(string username)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    return !conn.tblHealthcareUserDatas.Any(x => x.Username == username);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

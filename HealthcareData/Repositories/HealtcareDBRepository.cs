﻿using HealthcareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareData.Repositories
{
    class HealtcareDBRepository
    {
        public bool TryAddNewUserData(tblHealthcareUserData userData)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    conn.tblHealthcareUserDatas.Add(userData);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryAddNewPatient(tblClinicPatient patient)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    conn.tblClinicPatients.Add(patient);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryAddNewDoctor(tblClinicDoctor doctor)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    conn.tblClinicDoctors.Add(doctor);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryAddNewManager(tblClinicManager manager)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    conn.tblClinicManagers.Add(manager);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryAddNewMaintenance(tblClinicMaintenance maintenance)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    conn.tblClinicMaintenances.Add(maintenance);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryAddNewSickLeaveRequirement(tblSickLeaveRequirement sickLeaveRequirement)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    conn.tblSickLeaveRequirements.Add(sickLeaveRequirement);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetUserDataId(string username)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    var user = conn.tblHealthcareUserDatas.FirstOrDefault(x => x.Username == username);
                    if (user != null)
                        return user.UserDataID;
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public tblClinicDoctor LoadDoctor(int doctorId)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    return conn.tblClinicDoctors.FirstOrDefault(x => x.ClinicDoctorID == doctorId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<tblClinicDoctor> LoadDoctors()
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    if (conn.tblClinicDoctors.Any())                    
                        return conn.tblClinicDoctors.Include(x => x.UserDataID).ToList();                  
                    return new List<tblClinicDoctor>();
                }
            }
            catch (Exception)
            {
                return new List<tblClinicDoctor>();
            }
        }
        public List<tblClinicPatient> LoadPatients()
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    if (conn.tblClinicPatients.Any())
                        return conn.tblClinicPatients.Include(x => x.UserDataID).ToList();
                    return new List<tblClinicPatient>();
                }
            }
            catch (Exception)
            {
                return new List<tblClinicPatient>();
            }
        }

        public List<tblClinicManager> LoadManagers()
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    if (conn.tblClinicManagers.Any())
                        return conn.tblClinicManagers.Include(x => x.UserDataID).ToList();
                    return new List<tblClinicManager>();
                }
            }
            catch (Exception)
            {
                return new List<tblClinicManager>();
            }
        }

        public List<tblClinicMaintenance> LoadMaintenances()
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    if (conn.tblClinicMaintenances.Any())
                        return conn.tblClinicMaintenances.Include(x => x.UserDataID).ToList();
                    return new List<tblClinicMaintenance>();
                }
            }
            catch (Exception)
            {
                return new List<tblClinicMaintenance>();
            }
        }

    }
}

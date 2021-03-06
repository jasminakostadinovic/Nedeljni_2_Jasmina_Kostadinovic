﻿using HealthcareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Windows;

namespace HealthcareData.Repositories
{
    public class HealtcareDBRepository
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
        public bool TryAddNewAdministrator(tblClinicAdministrator administrator)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    conn.tblClinicAdministrators.Add(administrator);
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

        public List<tblHealthcareInstitution> LoadHealthcareInstitutions()
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    if (conn.tblHealthcareInstitutions.Any())
                        return conn.tblHealthcareInstitutions.ToList();
                    return new List<tblHealthcareInstitution>();
                }
            }
            catch (Exception)
            {
                return new List<tblHealthcareInstitution>();
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

        public List<tblClinicAdministrator> LoadAdministrators()
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    if (conn.tblClinicAdministrators.Any())
                        return conn.tblClinicAdministrators.ToList();
                    return new List<tblClinicAdministrator>();
                }
            }
            catch (Exception)
            {
                return new List<tblClinicAdministrator>();
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

        public bool IsHealthcareInstitutionCreated()
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    return conn.tblHealthcareInstitutions.Any();
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
                    if (!conn.tblClinicMaintenances.Any())
                    {
                        conn.tblClinicMaintenances.Add(maintenance);
                        conn.SaveChanges();
                        return true;
                    }
                    else
                    {
                        if(conn.tblClinicMaintenances.Count() == 3)
                        {
                            var toRemove = conn.tblClinicMaintenances.First();
                            conn.tblClinicMaintenances.Remove(toRemove);
                            conn.tblClinicMaintenances.Add(maintenance);
                            conn.SaveChanges();
                            return true;
                        }
                        conn.tblClinicMaintenances.Add(maintenance);
                        conn.SaveChanges();
                        return true;
                    }
                
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public tblClinicMaintenance LoadMaintenanceByUserDataId(int userDataId)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    return conn.tblClinicMaintenances.FirstOrDefault(x => x.UserDataID == userDataId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public tblClinicManager LoadManagerByUserDataId(int userDataId)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    return conn.tblClinicManagers.FirstOrDefault(x => x.UserDataID == userDataId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool TryRemoveClinicMaintenance(int clinicMaintenanceID)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    var maintenanceToRemove = conn.tblClinicMaintenances
                        .FirstOrDefault(x => x.ClinicMaintenanceID == clinicMaintenanceID);

                    if (maintenanceToRemove != null)
                    {
                        conn.tblClinicMaintenances.Remove(maintenanceToRemove);
                        conn.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object LoadPatientByUserDataId(int userDataId)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    return conn.tblClinicPatients.FirstOrDefault(x => x.UserDataID == userDataId);
                }
            }
            catch (Exception)
            {
                return null;
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

        public tblClinicDoctor LoadDoctorByUserDataId(int userDataId)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    return conn.tblClinicDoctors.FirstOrDefault(x => x.UserDataID == userDataId);
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
                        return conn.tblClinicDoctors.Include(x => x.tblHealthcareUserData).ToList();
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
                        return conn.tblClinicPatients.Include(x => x.tblHealthcareUserData).ToList();
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
                        return conn.tblClinicManagers.Include(x => x.tblHealthcareUserData).ToList();
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
                    {
                        var list = conn.tblClinicMaintenances.Include(x => x.tblHealthcareUserData).ToList();
                        return list;
                    }
                      
                    return new List<tblClinicMaintenance>();
                }
            }
            catch (Exception)
            {
                return new List<tblClinicMaintenance>();
            }
        }

        public bool TryRemoveUserData(int userId)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    var userToRemove = conn.tblHealthcareUserDatas.FirstOrDefault(x => x.UserDataID == userId);

                    if (userToRemove != null)
                    {
                        conn.tblHealthcareUserDatas.Remove(userToRemove);
                        conn.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryAddNewInstitution(tblHealthcareInstitution institution)
        {
            try
            {
                using (var conn = new HealthcareSoftwareEntities())
                {
                    conn.tblHealthcareInstitutions.Add(institution);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}


using System;
using PatientManagementSystem.Interfaces.Models;

namespace PatientManagementSystem.Interfaces.Manager
{
	public interface IPatientRecordManager
	{
        Task<IEnumerable<IPatientRecordModel>> GetAllPatientRecords();

        Task<IEnumerable<IPatientRecordModel>> AddOrUpdateManyPatientRecords(List<PatientRecord> patientRecords);

        Task DeletePatientRecord(int id);
    }
}


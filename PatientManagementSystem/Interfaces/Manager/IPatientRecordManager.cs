using System;
using PatientManagementSystem.Interfaces.Models;

namespace PatientManagementSystem.Interfaces.Manager
{
	public interface IPatientRecordManager
	{
        Task<IEnumerable<IPatientRecordModel>> GetAllPatientRecords();

        Task AddManyPatientRecords(List<PatientRecord> patientRecords);

        Task<IPatientRecordModel> AddPatientRecord(IPatientRecordModel patientRecord);

        Task<IPatientRecordModel> UpdatePatientRecord(IPatientRecordModel patientRecord);

        Task DeletePatientRecord(int id);
    }
}


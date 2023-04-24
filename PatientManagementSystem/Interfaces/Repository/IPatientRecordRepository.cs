using System;
using PatientManagementSystem.Interfaces.Models;

namespace PatientManagementSystem.Interfaces.Repository
{
	public interface IPatientRecordRepository
	{
		// Get all patient records
		Task<List<IPatientRecordModel>> GetAllPatientRecords();

        // Add patient record
        Task<IPatientRecordModel> AddPatientRecords(IPatientRecordModel patientRecord);

        // Edit patient record
        Task UpdatePatientRecord(IPatientRecordModel patientRecord);

        // Delete patient record
        Task DeletePatientRecord(int patientRecordId);
    }
}


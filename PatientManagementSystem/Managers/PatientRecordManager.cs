using System;
using PatientManagementSystem.Interfaces.Manager;
using PatientManagementSystem.Interfaces.Models;
using PatientManagementSystem.Interfaces.Repository;
using service_listeningtools.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PatientManagementSystem.Managers
{
	public class PatientRecordManager : IPatientRecordManager
	{
		private readonly IPatientRecordRepository patientRecordRepository;
        private readonly ILogger<PatientRecordManager> logger;


        public PatientRecordManager(IPatientRecordRepository patientRecordRepository, ILogger<PatientRecordManager> logger)
		{
			this.patientRecordRepository = patientRecordRepository;
			this.logger = logger;
		}

		public async Task<IEnumerable<IPatientRecordModel>> GetAllPatientRecords()
		{
			IEnumerable<IPatientRecordModel> patientRecords = await this.patientRecordRepository.GetAllPatientRecords();

			return patientRecords;
		}

        public async Task<IEnumerable<IPatientRecordModel>> AddOrUpdateManyPatientRecords(List<PatientRecord> patientRecords)
		{
			List<PatientRecord> uploadedPatientRecords = new List<PatientRecord>();
			bool uploadSuccess = false;

            foreach (var patientRecord in patientRecords)
            {
				if(patientRecord.Id == -1)
				{
					uploadSuccess = await this.patientRecordRepository.AddPatientRecords(patientRecord);
				} else {
                    uploadSuccess = await this.patientRecordRepository.UpdatePatientRecord(patientRecord);
                }

				if(uploadSuccess == true)
				{
					uploadedPatientRecords.Add(patientRecord);
				}
            }

			return uploadedPatientRecords;
        }

		public async Task DeletePatientRecord(int id)
		{
			await this.patientRecordRepository.DeletePatientRecord(id);
        }
    }
}


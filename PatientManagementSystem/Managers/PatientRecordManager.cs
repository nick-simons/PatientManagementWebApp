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

        public async Task AddManyPatientRecords(List<PatientRecord> patientRecords)
		{
            foreach (var patientRecord in patientRecords)
            {
                await this.patientRecordRepository.AddPatientRecords(patientRecord);
            }
        }


        public async Task<IPatientRecordModel> AddPatientRecord(IPatientRecordModel patientRecord)
		{
			patientRecord = await this.patientRecordRepository.AddPatientRecords(patientRecord);

			return patientRecord;
		}

        public async Task<IPatientRecordModel> UpdatePatientRecord(IPatientRecordModel patientRecord)
        {
            await this.patientRecordRepository.UpdatePatientRecord(patientRecord);

            return patientRecord;
        }

		public async Task DeletePatientRecord(int id)
		{
			await this.patientRecordRepository.DeletePatientRecord(id);
        }
    }
}


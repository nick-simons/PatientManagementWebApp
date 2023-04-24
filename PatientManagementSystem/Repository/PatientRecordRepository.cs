namespace service_listeningtools.Repository
{
    using Microsoft.Extensions.Configuration;
    using PatientManagementSystem;
    using PatientManagementSystem.Controllers;
    using PatientManagementSystem.Interfaces.Models;
    using PatientManagementSystem.Interfaces.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    public class PatientRecordRepository : IPatientRecordRepository
    {
        private readonly ILogger<PatientRecordRepository> logger;
        private readonly string connectionString;
        private readonly PatientRecordContext dbContext;

        private const string dbConnectionString = "PatientRecordContext";
        private const string SprocAddPatientRecord = "PatientRecord_AddPatientRecord";
        private const string SprocEditPatientRecord = "PatientRecord_EditPatientRecord";
        private const string SprocDeletePatientRecord = "PatientRecord_DeletePatientRecord";
        private const string SprocGetAllPatientRecords = "PatientRecord_GetAllPatientRecords";

        public PatientRecordRepository(PatientRecordContext dbContext, ILogger<PatientRecordRepository> logger, IConfiguration settings)
        {
            this.connectionString = settings.GetConnectionString(dbConnectionString);
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<IPatientRecordModel>> GetAllPatientRecords()
        {
            this.logger.Log(LogLevel.Information, $"Getting All Patient Records");
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand getCommand = new SqlCommand(SprocGetAllPatientRecords, connection))
                    {
                        getCommand.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await getCommand.ExecuteReaderAsync())
                        {
                            return await this.CreatePatientRecords(reader);
                        }

                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }

        public async Task<IPatientRecordModel> AddPatientRecords(IPatientRecordModel patientRecord)
        {
            this.logger.Log(LogLevel.Information, $"Adding new patient");

            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand addCommand = new SqlCommand(SprocAddPatientRecord, connection))
                    {
                        addCommand.CommandType = CommandType.StoredProcedure;

                        addCommand.Parameters.Add(new SqlParameter("@FirstName", patientRecord.FirstName));
                        addCommand.Parameters.Add(new SqlParameter("@LastName", patientRecord.LastName));
                        addCommand.Parameters.Add(new SqlParameter("@BirthDate", patientRecord.BirthDate));
                        addCommand.Parameters.Add(new SqlParameter("@Gender", patientRecord.Gender));

                        using (SqlDataReader reader = await addCommand.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();

                            patientRecord.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        }

                    }
                }
                return patientRecord;
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }

        public async Task UpdatePatientRecord(IPatientRecordModel patientRecord)
        { 
            this.logger.Log(LogLevel.Information, $"Getting All Patient Records");
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand addCommand = new SqlCommand(SprocEditPatientRecord, connection))
                    {
                        addCommand.CommandType = CommandType.StoredProcedure;

                        addCommand.Parameters.Add(new SqlParameter("@RecordId", patientRecord.Id));
                        addCommand.Parameters.Add(new SqlParameter("@FirstName", patientRecord.FirstName));
                        addCommand.Parameters.Add(new SqlParameter("@LastName", patientRecord.LastName));
                        addCommand.Parameters.Add(new SqlParameter("@BirthDate", patientRecord.BirthDate));
                        addCommand.Parameters.Add(new SqlParameter("@Gender", patientRecord.Gender));

                        await addCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }

        public async Task DeletePatientRecord(int patientRecordId)
        {
            this.logger.Log(LogLevel.Information, $"Getting All Patient Records");
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    await connection.OpenAsync();


                    using (SqlCommand deleteCommand = new SqlCommand(SprocDeletePatientRecord, connection))
                    {
                        deleteCommand.CommandType = CommandType.StoredProcedure;
                        deleteCommand.Parameters.Add(new SqlParameter("@RecordId", patientRecordId));
                        await deleteCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }

        private async Task<List<IPatientRecordModel>> CreatePatientRecords(SqlDataReader reader)
        {
            List<IPatientRecordModel> patientRecords = new List<IPatientRecordModel>();

            int patientRecordIdOrd = reader.GetOrdinal("Id");
            int firstNameOrd = reader.GetOrdinal("FirstName");
            int lastNameOrd = reader.GetOrdinal("LastName");
            int birthDateOrd = reader.GetOrdinal("BirthDate");
            int genderOrd = reader.GetOrdinal("Gender");

            while (await reader.ReadAsync())
            {
                PatientRecord patientRecord = new PatientRecord
                {
                    Id = reader.GetInt32(patientRecordIdOrd),
                    FirstName = reader.IsDBNull(firstNameOrd)
                        ? null
                        : reader.GetString(firstNameOrd),
                    LastName = reader.IsDBNull(lastNameOrd)
                        ? null
                        : reader.GetString(lastNameOrd),
                    BirthDate = reader.IsDBNull(birthDateOrd)
                        ? new DateTime?()
                        : reader.GetDateTime(birthDateOrd),
                    Gender = reader.IsDBNull(genderOrd)
                        ? null
                        : reader.GetString(genderOrd)[0]
                };

                patientRecords.Add(patientRecord);
            }

            return patientRecords;
        }
    }
}

using System.Data.SqlClient;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Interfaces.Manager;
using PatientManagementSystem.Interfaces.Models;

namespace PatientManagementSystem.Controllers;

[ApiController]
[Route("api/patientrecords")]
public class PatientManagementController : ControllerBase
{

    private readonly ILogger<PatientManagementController> logger;
    private readonly IPatientRecordManager patientRecordManager;

    public PatientManagementController(ILogger<PatientManagementController> logger, IPatientRecordManager patientRecordManager)
    {
        this.logger = logger;
        this.patientRecordManager = patientRecordManager;
    }

    // GET: api/patientrecords
    [HttpGet]
    public async Task<IEnumerable<IPatientRecordModel>> GetPatientRecords()
    {
        return await this.patientRecordManager.GetAllPatientRecords();
    }

    // Takes in a CSV file and creates patient records for all users in the csv
    [HttpPost]
    public async Task AddPatientRecords([FromBody]List<PatientRecord> patientRecords)
    { 
        await this.patientRecordManager.AddManyPatientRecords(patientRecords);
    }

    // Updates individual patient record
    [HttpPut]
    [Route("{id}")]
    public async Task<IPatientRecordModel> UpdatePatientRecord(int id, string firstName, string lastName, DateTime birthDate, char gender)
    {
        IPatientRecordModel patientRecord = new PatientRecord();

        patientRecord.Id = id;
        patientRecord.FirstName = firstName;
        patientRecord.LastName = lastName;
        patientRecord.BirthDate = birthDate;
        patientRecord.Gender = gender;

        return await this.patientRecordManager.UpdatePatientRecord(patientRecord);
    }
    
    // Deletes individual patient records
    [HttpDelete]
    [Route("{id}")]
    public async Task DeletePatientRecord(int id)
    {
        await this.patientRecordManager.DeletePatientRecord(id);
    }
    
    
    
}



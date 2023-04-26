using System.Data.SqlClient;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Interfaces.Manager;
using PatientManagementSystem.Interfaces.Models;

namespace PatientManagementSystem.Controllers;

[ApiController]
[Route("api/patientrecords")]
[EnableCors("MyPolicy")]
[Produces("application/json")]
[Consumes("application/json")]
public class PatientManagementController : ControllerBase
{

    private readonly ILogger<PatientManagementController> logger;
    private readonly IPatientRecordManager patientRecordManager;

    public PatientManagementController(ILogger<PatientManagementController> logger, IPatientRecordManager patientRecordManager)
    {
        this.logger = logger;
        this.patientRecordManager = patientRecordManager;
    }

    // Returns all patient records
    [HttpGet]
    public async Task<IEnumerable<IPatientRecordModel>> GetPatientRecords()
    {
        return await this.patientRecordManager.GetAllPatientRecords();
    }

    // Takes in a list of patient records and saves them to the db
    [HttpPost]
    public async Task<IEnumerable<IPatientRecordModel>> AddOrUpdatePatientRecords([FromBody]List<PatientRecord> patientRecords)
    {
        return await this.patientRecordManager.AddOrUpdateManyPatientRecords(patientRecords);
    }
    
    // Deletes individual patient records
    [HttpDelete]
    [Route("{id}")]
    public async Task DeletePatientRecord(int id)
    {
        await this.patientRecordManager.DeletePatientRecord(id);
    }
}



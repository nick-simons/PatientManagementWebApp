using System;
namespace PatientManagementSystem.Interfaces.Models
{
	public interface IPatientRecordModel
	{
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public char? Gender { get; set; }
    }
}


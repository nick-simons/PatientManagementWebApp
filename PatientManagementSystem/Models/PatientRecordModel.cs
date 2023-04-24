using System;
using PatientManagementSystem.Interfaces.Models;

namespace PatientManagementSystem
{
	public class PatientRecord : IPatientRecordModel
	{
		public int Id { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public DateTime? BirthDate { get; set; }

		public char? Gender { get; set; }
	}
}


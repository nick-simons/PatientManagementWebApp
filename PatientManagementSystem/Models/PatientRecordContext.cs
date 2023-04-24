using System;
using Microsoft.EntityFrameworkCore;

namespace PatientManagementSystem
{
	public class PatientRecordContext : DbContext
	{
        public PatientRecordContext(DbContextOptions<PatientRecordContext> options)
        : base(options) { }

        public DbSet<PatientRecord> PatientRecords { get; set; } = null;
    }
}
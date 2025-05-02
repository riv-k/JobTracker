using SQLite;
using System;

namespace JobTracker.Data
{
    // DTO for database persistence
    public class JobApplicationDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Core fields (initialized to avoid null warnings)
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime? DateApplied { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Paths to files stored in AppData (nullable)
        public string? CVPath { get; set; }
        public string? CLPath { get; set; }

        // Parameterless constructor is sufficient with default initializers
        public JobApplicationDTO() { }
    }
}

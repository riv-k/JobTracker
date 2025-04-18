using SQLite;
using System;

namespace JobTracker.Models
{
    public class JobApplication
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public DateTime DateApplied { get; set; }
        public DateTime DateClosing { get; set; }

        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string? CvFilePath { get; set; }
        public string? CoverLetterFilePath { get; set; }

        public JobApplication() { }

        public JobApplication(
            string companyName,
            string jobTitle,
            string status,
            string location,
            DateTime dateApplied,
            DateTime dateClosing,
            string link,
            string description,
            string? cvFilePath,
            string? coverLetterFilePath)
        {
            CompanyName = companyName;
            JobTitle = jobTitle;
            Status = status;
            Location = location;
            DateApplied = dateApplied;
            DateClosing = dateClosing;
            Link = link;
            Description = description;
            CvFilePath = cvFilePath;
            CoverLetterFilePath = coverLetterFilePath;
        }
    }
}

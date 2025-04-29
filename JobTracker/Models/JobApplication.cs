using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Models;

public class JobApplication
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string JobTitle { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // "Interested", "Applied", etc.
    public DateTime DateApplied { get; set; }
    public string Link { get; set; } // Link to job posting
    public string ResumePath { get; set; } // Path to uploaded resume or cover letter
    public string Location { get; set; }

    // Parameterless constructor (required by SQLite)
    public JobApplication() { }

    // Constructor to initialize job application
    public JobApplication(string companyName, string jobTitle, string description, string status, DateTime dateApplied, string link, string resumePath, string location)
    {
        CompanyName = companyName;
        JobTitle = jobTitle;
        Description = description;
        Status = status;
        DateApplied = dateApplied;
        Link = link;
        ResumePath = resumePath;
        Location = location;
    }
}

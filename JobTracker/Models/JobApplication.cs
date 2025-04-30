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
    public string Location { get; set; }
    public DateTime DateApplied { get; set; }
    public DateTime ClosingDate { get; set; }
    public string Status { get; set; }
    public string Link { get; set; }
    public string Cv { get; set; } // CV file path or content
    public string? Cl { get; set; } // Cover Letter file path or content
    public string Description { get; set; }

    // Parameterless constructor (required by SQLite)
    public JobApplication() { }

    // Constructor to initialize job application
    public JobApplication(string companyName, string jobTitle, string location, DateTime dateApplied, DateTime closingDate,
                          string status, string link, string cv, string? cl, string description)
    {
        CompanyName = companyName;
        JobTitle = jobTitle;
        Location = location;
        DateApplied = dateApplied;
        ClosingDate = closingDate;
        Status = status;
        Link = link;
        Cv = cv;
        Cl = cl;
        Description = description;
    }

}

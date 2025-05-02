using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Models;

public class JobApplication
{
    public string CompanyName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public DateTime DateApplied { get; set; }
    public DateTime ClosingDate { get; set; }
    public string Status { get; set; }
    public string Link { get; set; }
    public IBrowserFile? Cv { get; set; } // CV file path 
    public IBrowserFile? Cl { get; set; } // Cover Letter file path 
    public string Description { get; set; }


    // Constructor to initialize job application
    public JobApplication(string companyName, string jobTitle, string location, DateTime dateApplied, DateTime closingDate,
                       string status, string link, IBrowserFile? cv, IBrowserFile? cl, string description)
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

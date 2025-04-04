﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Models;

public class JobApplication
{
    public string CompanyName { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // "Interested", "Applied", etc.
    public DateTime DateApplied { get; set; }
    public string Link { get; set; } // Link to job posting
    public string ResumePath { get; set; } // Path to uploaded resume or cover letter

    // Constructor to initialize job application
    public JobApplication(string companyName, string roleName, string description, string status, DateTime dateApplied, string link, string resumePath)
    {
        CompanyName = companyName;
        RoleName = roleName;
        Description = description;
        Status = status;
        DateApplied = dateApplied;
        Link = link;
        ResumePath = resumePath;
    }
}

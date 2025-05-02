using JobTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Models;

public class JobStatusContainer
{
    public string Status { get; set; }
    public List<JobApplicationDTO> JobApplications { get; set; }

    public JobStatusContainer(string status)
    {
        Status = status;
        JobApplications = new List<JobApplicationDTO>();
    }

    //add a job application
    public void AddJobApplication(JobApplicationDTO jobApplication)
    {
        JobApplications.Add(jobApplication);
    }

    //remove a job application
    public void RemoveJobApplication(JobApplicationDTO jobApplication)
    {
        JobApplications.Remove(jobApplication);
    }
}


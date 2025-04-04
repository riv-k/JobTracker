using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Models;

public class JobStatusContainer
{
    public string Status { get; set; }
    public List<JobApplication> JobApplications { get; set; }

    public JobStatusContainer(string status)
    {
        Status = status;
        JobApplications = new List<JobApplication>();
    }

    //add a job application
    public void AddJobApplication(JobApplication jobApplication)
    {
        JobApplications.Add(jobApplication);
    }

    //remove a job application
    public void RemoveJobApplication(JobApplication jobApplication)
    {
        JobApplications.Remove(jobApplication);
    }
}


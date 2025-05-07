using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Models;

public class JobSubmission
{
    public JobApplication Job { get; set; }
    public int? ExistingJobId { get; set; }

    public JobSubmission(JobApplication job, int? existingJobId = null)
    {
        Job = job;
        ExistingJobId = existingJobId;
    }
}

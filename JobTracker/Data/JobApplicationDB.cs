using JobTracker.Models;
using SQLite;

namespace JobTracker.Data;

public class JobApplicationDB
{
    SQLiteAsyncConnection database;

    // Initialize the database connection and create the table if it doesn't exist
    async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await database.CreateTableAsync<JobApplication>();
    }

    // Retrieve all job applications
    public async Task<List<JobApplication>> GetJobApplicationsAsync()
    {
        await Init();
        return await database.Table<JobApplication>().ToListAsync();
    }

    // Retrieve job applications by status 
    public async Task<List<JobApplication>> GetJobApplicationsByStatusAsync(string status)
    {
        await Init();
        return await database.Table<JobApplication>().Where(j => j.Status == status).ToListAsync();
    }

    // Retrieve a job application by ID
    public async Task<JobApplication> GetJobApplicationByIdAsync(int id)
    {
        await Init();
        return await database.Table<JobApplication>().Where(j => j.Id == id).FirstOrDefaultAsync();
    }

    // Save/Create/Update job application 
    public async Task<int> SaveJobApplicationAsync(JobApplication jobApplication)
    {
        await Init();
        if (jobApplication.Id != 0) 
            return await database.UpdateAsync(jobApplication);
        else
            return await database.InsertAsync(jobApplication);
    }

    // Delete job application
    public async Task<int> DeleteJobApplicationAsync(JobApplication jobApplication)
    {
        await Init();
        return await database.DeleteAsync(jobApplication);
    }
}

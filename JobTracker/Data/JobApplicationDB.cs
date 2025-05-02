using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Maui.Storage;
using SQLite;
using JobTracker.Models;



namespace JobTracker.Data;

public class JobApplicationDB
{
    private SQLiteAsyncConnection database;

    //public JobApplicationDB()
    //{
    //    // Initialize the database in the background
    //    _ = Init();
    //}

    private async Task Init()
    {
        if (database != null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await database.CreateTableAsync<JobApplicationDTO>();
    }


    // Retrieve all job applications
    public async Task<List<JobApplicationDTO>> GetJobApplicationsAsync()
    {
        await Init();
        return await database.Table<JobApplicationDTO>().ToListAsync();
    }

    // Retrieve a job application by ID
    public async Task<JobApplicationDTO?> GetJobApplicationByIdAsync(int id)
    {
        await Init();
        return await database.Table<JobApplicationDTO>()
                             .Where(j => j.Id == id)
                             .FirstOrDefaultAsync();
    }

    // Save a new job application
    public async Task SaveJobApplicationAsync(JobApplication app)
    {
        await Init();

        // Save any uploaded files and get local paths
        string? cvPath = null;
        string? clPath = null;

        if (app.Cv != null)
            cvPath = await SaveFileToLocalAsync(app.Cv, "cv");
        if (app.Cl != null)
            clPath = await SaveFileToLocalAsync(app.Cl, "cl");

        var dto = new JobApplicationDTO
        {
            CompanyName = app.CompanyName,
            JobTitle = app.JobTitle,
            Status = app.Status,
            Location = app.Location,
            DateApplied = app.DateApplied,
            ClosingDate = app.ClosingDate,
            Link = app.Link,
            Description = app.Description,
            CVPath = cvPath,
            CLPath = clPath
        };

        await database.InsertAsync(dto);
    }

    // Delete a job application by DTO ID
    public async Task<int> DeleteJobApplicationAsync(int id)
    {
        await Init();
        return await database.DeleteAsync<JobApplicationDTO>(id);
    }

    // Helper: saves an IBrowserFile to subfolder and returns the file path
    private async Task<string> SaveFileToLocalAsync(IBrowserFile file, string subfolder)
    {
        var folderPath = Path.Combine(FileSystem.AppDataDirectory, subfolder);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, file.Name);

        using var stream = file.OpenReadStream();
        using var fs = File.Create(filePath);
        await stream.CopyToAsync(fs);

        return filePath;
    }
}

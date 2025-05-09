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

    // Update an existing job application
    public async Task UpdateJobApplicationAsync(int id, JobApplication app)
    {
        Console.WriteLine("Updating Job Application");
        await Init();

        // Get the existing job application
        var existingJob = await GetJobApplicationByIdAsync(id);
        if (existingJob == null)
            throw new Exception($"Job application with ID {id} not found.");

        // Original paths
        string? originalCvPath = existingJob.CVPath;
        string? originalClPath = existingJob.CLPath;

        // New paths (default to original)
        string? newCvPath = originalCvPath;
        string? newClPath = originalClPath;

        // Handle CV file safely
        if (app.Cv != null && app.Cv is IBrowserFile cvFile)
        {
            try
            {
                DeleteFileIfExists(originalCvPath);
                newCvPath = await SaveFileToLocalAsync(app.Cv, "cv");
                Console.WriteLine($"New CV path: {newCvPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing CV file: {ex.Message}");
            }
        }


        // Handle Cover Letter file safely
        if (app.Cl != null && app.Cl is IBrowserFile clFile)
        {
            try
            {
                DeleteFileIfExists(originalClPath);
                newClPath = await SaveFileToLocalAsync(app.Cl, "cl");
                Console.WriteLine($"New CL path: {newClPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing Cover Letter file: {ex.Message}");
            }
        }


        // Update the existing record
        existingJob.CompanyName = app.CompanyName;
        existingJob.JobTitle = app.JobTitle;
        existingJob.Status = app.Status;
        existingJob.Location = app.Location;
        existingJob.DateApplied = app.DateApplied;
        existingJob.ClosingDate = app.ClosingDate;
        existingJob.Link = app.Link;
        existingJob.Description = app.Description;
        existingJob.CVPath = newCvPath;
        existingJob.CLPath = newClPath;

        await database.UpdateAsync(existingJob);
    }



    // Delete a job application by DTO ID
    public async Task<int> DeleteJobApplicationAsync(int id)
    {
        await Init();

        // Get the job application first to access its file paths
        var job = await GetJobApplicationByIdAsync(id);
        if (job != null)
        {
            // Delete associated files if they exist
            DeleteFileIfExists(job.CVPath);
            DeleteFileIfExists(job.CLPath);
        }

        // Then delete the database record
        return await database.DeleteAsync<JobApplicationDTO>(id);
    }

    // Helper: saves an IBrowserFile to subfolder and returns the file path
    private async Task<string> SaveFileToLocalAsync(IBrowserFile file, string subfolder)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        var folderPath = Path.Combine(FileSystem.AppDataDirectory, subfolder);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Generate a unique filename to avoid collisions
        var fileName = $"{DateTime.Now.Ticks}_{file.Name}";
        var filePath = Path.Combine(folderPath, fileName);

        try
        {
            using var stream = file.OpenReadStream(maxAllowedSize: 10485760); // 10MB max
            using var fs = File.Create(filePath);
            await stream.CopyToAsync(fs);
            return filePath;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file {fileName}: {ex.Message}");
            throw;
        }
    }

    // Helper method to safely delete a file if it exists
    private void DeleteFileIfExists(string? filePath)
    {
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine($"Deleted file: {filePath}");
            }
            catch (Exception ex)
            {
                // Log the error but continue (don't throw)
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
            }
        }
    }
}
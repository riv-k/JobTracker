using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using JobTracker.Models;


namespace JobTracker.Data;

internal class JobApplicationDatabase
{
    private SQLiteAsyncConnection _database;

    private async Task Init()
    {
        if ( _database is not null)
        {
            return;
        }

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<JobApplication>();
    }

    //  CREATE | UPDATE
    public async Task<int> SaveJobApplicationAsync(JobApplication application)
    {
        await Init();
        if (application.Id != 0)
            return await _database.UpdateAsync(application);
        else
            return await _database.InsertAsync(application);
    }

    // READ ALL | GET ALL
    public async Task<List<JobApplication>> GetJobApplicationAsync()
    {
        await Init();
        return await _database.Table<JobApplication>().ToListAsync();
    }

    // READ ONE | GET ONE
    public async Task<JobApplication> GetJobApplicationAsync(int id)
    {
        await Init();
        return await _database.Table<JobApplication>()
                                .Where(j => j.Id == id)
                                .FirstOrDefaultAsync();
    }

    // DELETE
    public async Task<int> DeleteJobApplicationAsync(JobApplication application)
    {
        await Init();
        return await _database.DeleteAsync(application);
    }
}

using GymProject.DataAccess.Data.Contexts;
using GymProject.DataAccess.Models;
using GymProject.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Repositories.Classes
{
    public class SessionRepository(AppDbContext dbContext) : Repository<Session>(dbContext), ISessionRepository
    {
        public async Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategory(CancellationToken ct = default)
        {
            var query = dbContext.Sessions.AsNoTracking().Include(s => s.Trainer).Include(s => s.Category);
            return await query.ToListAsync(ct);
        }

        public async Task<int> GetCountOfBookedSlotsAsync(int id, CancellationToken ct = default)
        {
            return await dbContext.Bookings.CountAsync(b => b.SessionId == id);
        }
    }
}

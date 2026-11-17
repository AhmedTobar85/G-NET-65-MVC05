using GymProject.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Repositories.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategory(CancellationToken ct = default);
        Task<int> GetCountOfBookedSlotsAsync(int id, CancellationToken ct = default);
    }
}

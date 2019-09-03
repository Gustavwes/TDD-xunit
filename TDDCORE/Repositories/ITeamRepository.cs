using System;
using System.Collections.Generic;
using System.Text;
using TDDCORE.Models;

namespace TDDCORE.Repositories
{
    public interface ITeamRepository
    {
        List<TeamStats> GetTeamStats(int seasonId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDDCORE.Repositories;

namespace TDDCORE.Operators
{
    public class TeamStatCalculator
    {
        private ITeamRepository _teamRepository;

        public TeamStatCalculator(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public int GetTotalGoalsForSeason(int seasonId)
        {
            var allTeams = _teamRepository.GetTeamStats(seasonId);

            return allTeams.Sum(x => x.GoalsFor);
        }
    }
}

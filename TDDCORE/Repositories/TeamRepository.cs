using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDDCORE.Models;

namespace TDDCORE.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public List<TeamStats> GetTeamStats(int seasonId)
        {           
 
            var team1 = new TeamStats() { GoalsFor = 52, SeasonId = 1, TeamName = "Manchester City" };
            var team2 = new TeamStats() { GoalsFor = 23, SeasonId = 1, TeamName = "Manchester United" };
            var team3 = new TeamStats() { GoalsFor = 46, SeasonId = 1, TeamName = "Liverpool" };

            var teamList = new List<TeamStats>();
            teamList.Add(team1);
            teamList.Add(team2);
            teamList.Add(team3);

            return teamList.Where(x => x.SeasonId == seasonId).ToList();
        }
    }
}

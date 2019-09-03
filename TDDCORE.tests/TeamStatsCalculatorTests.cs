using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TDDCORE.Model;
using TDDCORE.Repositories;
using TDDCORE.Operators;
using Xunit;

namespace TDDCORE.tests
{
    public class TeamStatsCalculatorTests
    {

        [Fact]
        public void GetTotalGoalsForSeason_Returns_Expected_Goal_Count()
        {
            //Arrange
            var mockTeamRepo = new Mock<ITeamRepository>();

            //setup a mock stat repo to return some fake data in our target method
            mockTeamRepo.Setup(mtrsr => mtrsr.GetTeamStats(1)).Returns(new List<TeamStats>
               {
                  new TeamStats {SeasonId = 1,TeamName = "team 1",GoalsFor=1},
                  new TeamStats {SeasonId=1,TeamName = "team 2",GoalsFor=2},
                  new TeamStats {SeasonId = 1,TeamName = "team 3",GoalsFor=3}
               });

            //create TeamStatCalculator by injecting our mock repository
            var teamStatCalculator = new TeamStatCalculator(mockTeamRepo.Object);

            //Act
            var result = teamStatCalculator.GetTotalGoalsForSeason(1);

            //Assert
            Assert.True(result == 6);
        }
    }
}

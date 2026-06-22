using SoccerProjectCase5.WebUI.Models.Enums;

namespace SoccerProjectCase5.WebUI.Dtos.FixtureDtos
{
    public class DashboardFixtureDto
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }

        public int FullTimeHomeScore { get; set; }
        public int FullTimeAwayScore { get; set; }

        public int WeekNumber { get; set; }
        public MatchStatus Status { get; set; }
    }
}

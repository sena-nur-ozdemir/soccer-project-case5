using SoccerProjectCase5.WebUI.Models.Enums;

namespace SoccerProjectCase5.WebUI.Dtos.FixtureDtos
{
    public class FixtureDto
    {
        public int FixtureId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeLogoUrl { get; set; }
        public string AwayLogoUrl { get; set; }

        public int HalfTimeHomeScore { get; set; }
        public int HalfTimeAwayScore { get; set; }
        public int FullTimeHomeScore { get; set; }
        public int FullTimeAwayScore { get; set; }

        public string[] HomeTeamForm { get; set; }
        public string[] AwayTeamForm { get; set; }

        public string StadiumName { get; set; }
        public int WeekNumber { get; set; }

        public DateTime MatchDate { get; set; }
        public MatchStatus Status { get; set; }
    }
}
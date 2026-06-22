using SoccerProjectCase5.WebUI.Models.Enums;

namespace SoccerProjectCase5.WebUI.Dtos.FixtureDtos
{
    public class CreateFixtureDto
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public int HalfTimeHomeScore { get; set; }
        public int HalfTimeAwayScore { get; set; }
        public int FullTimeHomeScore { get; set; }
        public int FullTimeAwayScore { get; set; }

        public string StadiumName { get; set; }
        public DateTime MatchDate { get; set; }
        public int WeekNumber { get; set; }
        public MatchStatus Status { get; set; }
    }
}

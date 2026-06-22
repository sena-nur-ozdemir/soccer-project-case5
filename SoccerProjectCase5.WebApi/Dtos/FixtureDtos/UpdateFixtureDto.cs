using SoccerProjectCase5.WebApi.Enums;

namespace SoccerProjectCase5.WebApi.Dtos.FixtureDtos
{
    public class UpdateFixtureDto
    {
        public int FixtureId { get; set; }
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

using SoccerProjectCase5.WebApi.Enums;

namespace SoccerProjectCase5.WebApi.Entities
{
    public class Fixture
    {
        public int FixtureId { get; set; }

        public int WeekNumber { get; set; }
        public string StadiumName { get; set; }
        public DateTime MatchDate { get; set; }
        public MatchStatus Status { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
      
        public int HalfTimeHomeScore { get; set; } // İlk yarı skoru [cite: 17]
        public int HalfTimeAwayScore { get; set; }
        public int FullTimeHomeScore { get; set; } // Maç sonu skoru [cite: 18]
        public int FullTimeAwayScore { get; set; }

        public ICollection<MatchEvent>? Events { get; set; } 
        public MatchStatistic? Statistic { get; set; }
    }
}

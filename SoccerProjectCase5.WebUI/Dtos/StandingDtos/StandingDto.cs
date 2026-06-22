namespace SoccerProjectCase5.WebUI.Dtos.StandingDtos
{
    public class StandingDto
    {
        public int TeamId { get; set; }
        public int Pos { get; set; }
        public string ShortName { get; set; }
        public string TeamName { get; set; }
        public string LogoUrl { get; set; }

        public int MatchesPlayed { get; set; }

        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }

        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        public int GoalDifference { get; set; }
        public int Points { get; set; }

        public List<string> Form { get; set; } = new();
    }
}
using SoccerProjectCase5.WebUI.Dtos.FixtureDtos;

namespace SoccerProjectCase5.WebUI.Models
{
    public class IndexFixtureViewModel
    {
        public int SelectedWeek { get; set; }
        public int TotalMatchCount { get; set; }
        public int LiveCount { get; set; }
        public int FinishedCount { get; set; }
        public int UpcomingCount { get; set; }

        public FixtureDto? featuredMatch { get; set; }

        public List<FixtureDto> LiveMatches { get; set; }
        public List<FixtureDto> FinishedMatches { get; set; }
        public List<FixtureDto> UpcomingMatches { get; set; }
    }
}
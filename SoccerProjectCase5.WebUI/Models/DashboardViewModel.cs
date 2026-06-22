using SoccerProjectCase5.WebUI.Dtos.FixtureDtos;

namespace SoccerProjectCase5.WebUI.Models
{
    public class DashboardViewModel
    {
        public int TotalFixture { get; set; }
        public int TotalMatchEvent { get; set; }
        public int TotalMatchStatistic { get; set; }
        public int TotalTeam { get; set; }
        public List<DashboardFixtureDto> Last5Matches { get; set; }
    }
}

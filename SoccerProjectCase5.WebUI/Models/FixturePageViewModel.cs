using SoccerProjectCase5.WebUI.Dtos.FixtureDtos;

namespace SoccerProjectCase5.WebUI.Models
{
    public class FixturePageViewModel
    {
        public int WeekNumber { get; set; }
        public int TotalCount { get; set; }
        public int UpcomingCount { get; set; }
        public int FinishedCount { get; set; }
        public string MatchDaysText { get; set; }
        public DateTime? NextMatchDate { get; set; }

        // Maçları gün gün ayırarak göstermek için IGrouping kullandık
        public List<IGrouping<DateTime, FixtureDto>> FixturesByDate { get; set; }
    }
}
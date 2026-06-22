using SoccerProjectCase5.WebUI.Models.Enums;

namespace SoccerProjectCase5.WebUI.Dtos.MatchEventDtos
{
    public class CreateMatchEventDto
    {
        public int FixtureId { get; set; }
        public int TeamId { get; set; }
        public string PlayerName { get; set; }
        public MatchEventType EventType { get; set; }
        public string? EventDescription { get; set; }
        public int Minute { get; set; }
    }
}

using SoccerProjectCase5.WebApi.Enums;

namespace SoccerProjectCase5.WebApi.Dtos.MatchEventDtos
{
    public class MatchEventDto
    {
        public int TeamId { get; set; }
        public string PlayerName { get; set; }

        public MatchEventType EventType { get; set; }
        public string? EventDescription { get; set; }
        public int Minute { get; set; }
    }
}

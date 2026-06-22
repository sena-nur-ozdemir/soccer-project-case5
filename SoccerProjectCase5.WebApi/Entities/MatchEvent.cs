using SoccerProjectCase5.WebApi.Enums;

namespace SoccerProjectCase5.WebApi.Entities
{
    public class MatchEvent
    {
        public int MatchEventId { get; set; }

        public int FixtureId { get; set; }
        public Fixture Fixture { get; set; }

        // Olayın hangi takıma ait olduğu
        public int TeamId { get; set; }

        public string PlayerName { get; set; }
        public int Minute { get; set; }

        public MatchEventType EventType { get; set; }
        public string? EventDescription { get; set; }
    }
}
using SoccerProjectCase5.WebUI.Dtos.FixtureDtos;

namespace SoccerProjectCase5.WebUI.Models
{
    public class TeamMatch
    {
        public int TeamId { get; set; }
        public FixtureDto Match { get; set; }
        public bool IsHome { get; set; }
    }
}
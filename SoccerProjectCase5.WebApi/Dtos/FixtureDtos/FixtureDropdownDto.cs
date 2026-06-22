namespace SoccerProjectCase5.WebApi.Dtos.FixtureDtos
{
    public class FixtureDropdownDto
    {
        public int FixtureId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int WeekNumber { get; set; }
    }
}

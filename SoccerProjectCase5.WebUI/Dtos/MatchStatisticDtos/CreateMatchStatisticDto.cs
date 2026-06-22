namespace SoccerProjectCase5.WebUI.Dtos.MatchStatisticDtos
{
    public class CreateMatchStatisticDto
    {
        public int FixtureId { get; set; }

        public int PossessionHome { get; set; }
        public int PossessionAway { get; set; }

        public int ShotsHome { get; set; }
        public int ShotsAway { get; set; }

        public int ShotsOnTargetHome { get; set; }
        public int ShotsOnTargetAway { get; set; }

        public int PassesHome { get; set; }
        public int PassesAway { get; set; }

        public int PassAccuracyHome { get; set; }
        public int PassAccuracyAway { get; set; }

        public int CornersHome { get; set; }
        public int CornersAway { get; set; }

        public int FoulsHome { get; set; }
        public int FoulsAway { get; set; }

        public int OffsidesHome { get; set; }
        public int OffsidesAway { get; set; }

        public int YellowCardsHome { get; set; }
        public int YellowCardsAway { get; set; }

        public int RedCardsHome { get; set; }
        public int RedCardsAway { get; set; }
    }
}

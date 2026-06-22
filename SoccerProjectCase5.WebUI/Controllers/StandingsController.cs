using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoccerProjectCase5.WebUI.Dtos.FixtureDtos;
using SoccerProjectCase5.WebUI.Dtos.StandingDtos;
using SoccerProjectCase5.WebUI.Dtos.TeamDtos;
using SoccerProjectCase5.WebUI.Models;
using SoccerProjectCase5.WebUI.Models.Enums;

namespace SoccerProjectCase5.WebUI.Controllers
{
    public class StandingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StandingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> StandingList()
        {
            var client = _httpClientFactory.CreateClient();

            var teamResponse = await client.GetAsync("https://localhost:7290/api/Teams");

            if (!teamResponse.IsSuccessStatusCode)
                return View(new List<StandingDto>());

            var teams = JsonConvert.DeserializeObject<List<ResultTeamDto>>(await teamResponse.Content.ReadAsStringAsync()) ?? new();

            var fixtureResponse = await client.GetAsync("https://localhost:7290/api/Fixtures");
            if (!fixtureResponse.IsSuccessStatusCode)
                return View(new List<StandingDto>());

            var fixtures = JsonConvert.DeserializeObject<List<FixtureDto>>(
                await fixtureResponse.Content.ReadAsStringAsync()
            ) ?? new();

            var completed = fixtures.Where(x => x.Status == MatchStatus.Ended).ToList();

            var matchesByTeam = completed.SelectMany(x => new List<TeamMatch>
                {
                    new TeamMatch { TeamId = x.HomeTeamId, Match = x, IsHome = true },
                    new TeamMatch { TeamId = x.AwayTeamId, Match = x, IsHome = false }
               })
               .GroupBy(x => x.TeamId)
               .ToDictionary(x => x.Key, x => x.ToList());

            var standings = teams.Select(team =>
            {
                
                matchesByTeam.TryGetValue(team.TeamId, out var matches);
                matches ??= new List<TeamMatch>();

                int wins = 0, draws = 0, loses = 0, gf = 0, ga = 0;

                var orderedMatches = matches
                    .Select(x => x.Match)
                    .OrderByDescending(x => x.MatchDate)
                    .ToList();

                foreach (var x in matches)
                {
                    var m = x.Match;
                    
                    if (x.TeamId == m.HomeTeamId)
                    {
                        gf += m.FullTimeHomeScore;
                        ga += m.FullTimeAwayScore;

                        if (m.FullTimeHomeScore > m.FullTimeAwayScore) wins++;
                        else if (m.FullTimeHomeScore == m.FullTimeAwayScore) draws++;
                        else loses++;
                    }
                    else
                    {
                        gf += m.FullTimeAwayScore;
                        ga += m.FullTimeHomeScore;

                        if (m.FullTimeAwayScore > m.FullTimeHomeScore) wins++;
                        else if (m.FullTimeAwayScore == m.FullTimeHomeScore) draws++;
                        else loses++;
                    }
                }

                // Son 5 maç formu
                var form = orderedMatches
                 .OrderBy(x => x.MatchDate)
                 .TakeLast(5)
                 .Select(m =>
                 {
                     bool isHome = m.HomeTeamId == team.TeamId;

                     var teamScore = isHome ? m.FullTimeHomeScore : m.FullTimeAwayScore;
                     var opponentScore = isHome ? m.FullTimeAwayScore : m.FullTimeHomeScore;

                     return teamScore > opponentScore ? "G"
                          : teamScore == opponentScore ? "B"
                          : "M";
                 })
                 .ToList();

                return new StandingDto
                {
                    TeamId = team.TeamId,
                    TeamName = team.TeamName,
                    LogoUrl = team.LogoUrl,
                    // Eğer takım ismi 3 harften kısaysa patlamaması için küçük bir güvenlik kontrolü eklendi
                    ShortName = team.TeamName.Length >= 3 ? team.TeamName.Substring(0, 3).ToUpper() : team.TeamName.ToUpper(),

                    MatchesPlayed = matches.Count,
                    Wins = wins,
                    Draws = draws,
                    Losses = loses,

                    GoalsFor = gf,
                    GoalsAgainst = ga,
                    GoalDifference = gf - ga,
                    Points = wins * 3 + draws,
                    Form = form
                };
            })
                 .OrderByDescending(x => x.Points)
                 .ThenByDescending(x => x.GoalDifference)
                 .ThenByDescending(x => x.GoalsFor)
                 .ToList();

            // Puanı aynı olan takımlar için 1., 2. gibi sıra (Pos) ataması
            for (int i = 0; i < standings.Count; i++)
            {
                standings[i].Pos = i + 1;
            }

            return View(standings);
        }
    }
}
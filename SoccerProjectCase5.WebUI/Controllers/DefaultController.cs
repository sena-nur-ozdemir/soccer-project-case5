using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoccerProjectCase5.WebUI.Dtos.FixtureDtos;
using SoccerProjectCase5.WebUI.Models;
using SoccerProjectCase5.WebUI.Models.Enums;

namespace SoccerProjectCase5.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int weekNumber = 34)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7290/api/Fixtures/GetFixturesByWeek/{weekNumber}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<FixtureDto>>(jsonData)
                                ?? new List<FixtureDto>();

                var model = new IndexFixtureViewModel
                {
                    SelectedWeek = weekNumber,
                    TotalMatchCount = values.Count,

                    LiveCount = values.Count(x => x.Status == MatchStatus.Ongoing),
                    FinishedCount = values.Count(x => x.Status == MatchStatus.Ended),
                    UpcomingCount = values.Count(x => x.Status == MatchStatus.NotPlayed),

                    featuredMatch = values.FirstOrDefault(x => x.Status == MatchStatus.Ended),

                    LiveMatches = values.Where(x => x.Status == MatchStatus.Ongoing).ToList(),
                    FinishedMatches = values.Where(x => x.Status == MatchStatus.Ended).ToList(),
                    UpcomingMatches = values.Where(x => x.Status == MatchStatus.NotPlayed).ToList(),
                };

                return View(model);
            }
            return View(new IndexFixtureViewModel()); // Hata alırsak boş model dönsün ki sayfa patlamasın
        }
    }
}
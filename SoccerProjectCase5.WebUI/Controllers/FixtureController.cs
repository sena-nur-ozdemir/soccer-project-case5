using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoccerProjectCase5.WebUI.Dtos.FixtureDtos;
using SoccerProjectCase5.WebUI.Dtos.MatchEventDtos;
using SoccerProjectCase5.WebUI.Models;
using SoccerProjectCase5.WebUI.Models.Enums;

namespace SoccerProjectCase5.WebUI.Controllers
{
    public class FixtureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FixtureController(IHttpClientFactory httpClientFactory)
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
                var values = JsonConvert.DeserializeObject<List<FixtureDto>>(jsonData) ?? new List<FixtureDto>();

                // Maçları tarihlerine göre grupluyoruz (Örn: Cumartesi maçları, Pazar maçları)
                var groupedFixtures = values.GroupBy(x => x.MatchDate.Date).OrderBy(x => x.Key).ToList();

                var firstDate = values.Any() ? values.Min(x => x.MatchDate) : (DateTime?)null;
                var lastDate = values.Any() ? values.Max(x => x.MatchDate) : (DateTime?)null;
                var nextMatch = values.Where(x => x.MatchDate > DateTime.Now).OrderBy(x => x.MatchDate).FirstOrDefault()
                                    ?? values.OrderBy(x => x.MatchDate).FirstOrDefault();

                var model = new FixturePageViewModel
                {
                    WeekNumber = weekNumber,
                    FixturesByDate = groupedFixtures,
                    TotalCount = values.Count(),
                    MatchDaysText = values.Any() ? $"{firstDate:ddd} - {lastDate:ddd}" : "-",

                    UpcomingCount = values.Count(x => x.Status == MatchStatus.NotPlayed),
                    FinishedCount = values.Count(x => x.Status == MatchStatus.Ended),
                    NextMatchDate = nextMatch?.MatchDate,
                };

                return View(model);
            }

            // Eğer API'ye ulaşılamazsa (hata olursa) sayfanın çökmemesi için boş model dönüyoruz
            return View(new FixturePageViewModel());
        }

        public async Task<IActionResult> GetDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7290/api/Fixtures/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetFixtureByIdDto>(jsonData);

                // HATA ÖNLEYİCİ: Eğer MatchEvents null gelirse boş liste ata
                if (value != null && value.MatchEvents == null)
                {
                    value.MatchEvents = new List<MatchEventDto>();
                }

                return View(value);
            }
            return View();
        }
    }
}
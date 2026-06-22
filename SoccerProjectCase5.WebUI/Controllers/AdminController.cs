using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SoccerProjectCase5.WebUI.Dtos.FixtureDtos;
using SoccerProjectCase5.WebUI.Dtos.MatchEventDtos;
using SoccerProjectCase5.WebUI.Dtos.MatchStatisticDtos; 
using SoccerProjectCase5.WebUI.Dtos.TeamDtos;
using SoccerProjectCase5.WebUI.Models;
using System.Text;

namespace SoccerProjectCase5.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Dashboard()
        {
            var client = _httpClientFactory.CreateClient();

            var fixturesResponse = await client.GetAsync("https://localhost:7290/api/Fixtures");
            var fixturesData = await fixturesResponse.Content.ReadAsStringAsync();
            var fixtures = JsonConvert.DeserializeObject<List<FixtureDto>>(fixturesData) ?? new List<FixtureDto>();

            var matchEventsResponse = await client.GetAsync("https://localhost:7290/api/MatchEvents");
            var eventData = await matchEventsResponse.Content.ReadAsStringAsync();
            var matchEvents = JsonConvert.DeserializeObject<List<MatchEventDto>>(eventData) ?? new List<MatchEventDto>();

            var matchStatsResponse = await client.GetAsync("https://localhost:7290/api/MatchStatistics");
            var statData = await matchStatsResponse.Content.ReadAsStringAsync();
            var matchStatistics = JsonConvert.DeserializeObject<List<MatchStatisticDto>>(statData) ?? new List<MatchStatisticDto>();

            var model = new DashboardViewModel
            {
                TotalFixture = fixtures.Count,
                TotalMatchEvent = matchEvents.Count,
                TotalMatchStatistic = matchStatistics.Count, 
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateFixture()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7290/api/Teams");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<TeamDto>>(jsonData);
                ViewBag.Teams = values.Select(x => new SelectListItem
                {
                    Text = x.TeamName, 
                    Value = x.TeamId.ToString(), 
                }).ToList();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFixture(CreateFixtureDto createFixtureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFixtureDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7290/api/Fixtures", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Dashboard");
            }
            return View(createFixtureDto);
        }

        public async Task<IActionResult> CreateMatchEvent()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7290/api/Fixtures/FixtureDropdown");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<FixtureDropdownDto>>(jsonData);
                ViewBag.Fixtures = values.Select(x => new SelectListItem
                {
                    Text = $"{x.HomeTeamName} vs {x.AwayTeamName} (Hafta {x.WeekNumber})",
                    Value = x.FixtureId.ToString()
                }).ToList();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchEvent(CreateMatchEventDto createMatchEventDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMatchEventDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7290/api/MatchEvents", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Dashboard");
            }
            return View(createMatchEventDto);
        }

        // CORS nedeniyle API çağrısını proxy olarak yapıyorum, fixture takımlarını UI’a aktarmak için.
        [HttpGet]
        public async Task<IActionResult> GetFixtureTeams(int fixtureId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7290/api/Fixtures/GetFixtureTeams/{fixtureId}");

            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FixtureTeamDto>(data);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateMatchStatistics()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7290/api/Fixtures/FixtureDropdown");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<FixtureDropdownDto>>(jsonData);
                ViewBag.Fixtures = values.Select(x => new SelectListItem
                {
                    Text = $"{x.HomeTeamName} vs {x.AwayTeamName} (Hafta {x.WeekNumber})",
                    Value = x.FixtureId.ToString()
                }).ToList();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchStatistics(CreateMatchStatisticDto createMatchStatisticDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMatchStatisticDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7290/api/MatchStatistics", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Dashboard");
            }
            return View(createMatchStatisticDto);
        }
    }
}
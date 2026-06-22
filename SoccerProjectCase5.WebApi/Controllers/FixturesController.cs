using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerProjectCase5.WebApi.Context;
using SoccerProjectCase5.WebApi.Dtos.FixtureDtos;
using SoccerProjectCase5.WebApi.Dtos.MatchEventDtos;
using SoccerProjectCase5.WebApi.Dtos.MatchStatisticDtos;
using SoccerProjectCase5.WebApi.Entities;
using SoccerProjectCase5.WebApi.Enums;

namespace SoccerProjectCase5.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public FixturesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> FixtureList()
        {
            var values = await _context.Fixtures.Include(x => x.HomeTeam).Include(x => x.AwayTeam).ToListAsync();
            return Ok(values);
        }

        [HttpGet("FixtureDropdown")]
        public async Task<IActionResult> FixtureDropdown()
        {
            var values = await _context.Fixtures.Include(x => x.HomeTeam).Include(x => x.AwayTeam).Select(x => new FixtureDropdownDto
            {
                FixtureId = x.FixtureId,
                HomeTeamName = x.HomeTeam.TeamName, 
                AwayTeamName = x.AwayTeam.TeamName,
                WeekNumber = x.WeekNumber
            }).ToListAsync();
            return Ok(values);
        }

        [HttpGet("GetFixtureTeams/{id}")]
        public async Task<IActionResult> GetFixtureTeams(int id)
        {
            var value = await _context.Fixtures.Include(x => x.HomeTeam).Include(x => x.AwayTeam)
                .Where(x => x.FixtureId == id).Select(x => new
                {
                    x.HomeTeamId,
                    HomeTeamName = x.HomeTeam.TeamName,
                    x.AwayTeamId,
                    AwayTeamName = x.AwayTeam.TeamName
                }).FirstOrDefaultAsync();
            return Ok(value);
        }

        [HttpGet("GetFixturesByWeek/{weekNumber}")]
        public async Task<IActionResult> GetFixturesByWeek(int weekNumber)
        {
            var fixtures = await _context.Fixtures.Include(x => x.HomeTeam).Include(x => x.AwayTeam)
                .Where(x => x.WeekNumber == weekNumber).ToListAsync();

            var finishedFixtures = await _context.Fixtures.Where(x => x.Status == MatchStatus.Ended).ToListAsync();

            var teamIds = finishedFixtures.SelectMany(x => new[] { x.HomeTeamId, x.AwayTeamId }).Distinct();
            var teamForms = new Dictionary<int, string[]>();

            foreach (var teamId in teamIds)
            {
                var form = finishedFixtures
                    .Where(x => x.HomeTeamId == teamId || x.AwayTeamId == teamId)
                    .OrderByDescending(x => x.MatchDate)
                    .Take(5)
                    .Select(x =>
                    {
                        bool isHome = x.HomeTeamId == teamId;

                        int teamScore = isHome ? x.FullTimeHomeScore : x.FullTimeAwayScore;
                        int opponentScore = isHome ? x.FullTimeAwayScore : x.FullTimeHomeScore;

                        if (teamScore > opponentScore) return "G";
                        if (teamScore < opponentScore) return "M";
                        return "B";
                    })
                    .ToArray();

                teamForms[teamId] = form;
            }

            var values = fixtures.Select(x => new ResultFixtureDto
            {
                FixtureId = x.FixtureId,
                HomeTeamId = x.HomeTeamId,
                AwayTeamId = x.AwayTeamId,
                HomeTeamName = x.HomeTeam.TeamName,
                AwayTeamName = x.AwayTeam.TeamName,
                HomeLogoUrl = x.HomeTeam.LogoUrl,
                AwayLogoUrl = x.AwayTeam.LogoUrl,
                HalfTimeHomeScore = x.HalfTimeHomeScore,
                HalfTimeAwayScore = x.HalfTimeAwayScore,
                FullTimeHomeScore = x.FullTimeHomeScore,
                FullTimeAwayScore = x.FullTimeAwayScore,
                StadiumName = x.StadiumName,
                MatchDate = x.MatchDate,
                Status = x.Status,
                HomeTeamForm = teamForms.TryGetValue(x.HomeTeamId, out var homeForm) ? homeForm : Array.Empty<string>(),
                AwayTeamForm = teamForms.TryGetValue(x.AwayTeamId, out var awayForm) ? awayForm : Array.Empty<string>()
            }).ToList();

            return Ok(values);
        }

        [HttpGet("GetFixtures")]
        public async Task<IActionResult> GetFixtures(MatchStatus? matchStatus, int? weekNumber)
        {
            var query = _context.Fixtures.Include(x => x.HomeTeam).Include(x => x.AwayTeam).AsQueryable();

            if (matchStatus.HasValue)
            {
                query = query.Where(x => x.Status == matchStatus.Value);
            }
            if (weekNumber.HasValue)
            {
                query = query.Where(x => x.WeekNumber == weekNumber);
            }

            var result = await query.Select(x => new ResultFixtureDto
            {
                FixtureId = x.FixtureId,
                HomeTeamName = x.HomeTeam.TeamName,
                AwayTeamName = x.AwayTeam.TeamName,
                HomeLogoUrl = x.HomeTeam.LogoUrl,
                AwayLogoUrl = x.AwayTeam.LogoUrl,
                HalfTimeHomeScore = x.HalfTimeHomeScore,
                HalfTimeAwayScore = x.HalfTimeAwayScore,
                FullTimeHomeScore = x.FullTimeHomeScore,
                FullTimeAwayScore = x.FullTimeAwayScore,
                StadiumName = x.StadiumName,
                MatchDate = x.MatchDate,
                Status = x.Status
            }).ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFixture(CreateFixtureDto createFixtureDto)
        {
            var value = _mapper.Map<Fixture>(createFixtureDto);
            await _context.Fixtures.AddAsync(value);
            await _context.SaveChangesAsync();
            return Ok("Fixture added successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFixture(int id)
        {
            var value = await _context.Fixtures.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.Fixtures.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Fixture deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFixture(int id)
        {
            var value = await _context.Fixtures.Include(x => x.HomeTeam).Include(x => x.AwayTeam)
                                               .Include(x => x.Events).Include(x => x.Statistic)
                                               .FirstOrDefaultAsync(x => x.FixtureId == id);
            if (value == null)
            {
                return NotFound();
            }

            // Ana DTO'yu çeviriyoruz
            var mappedValue = _mapper.Map<GetFixtureByIdDto>(value);

            // BÜYÜK DÜZELTME BURADA: 
            // AutoMapper'ın isim farklılığından dolayı atladığı o iki listeyi zorla içine basıyoruz!
            mappedValue.MatchEvents = _mapper.Map<List<MatchEventDto>>(value.Events);
            mappedValue.MatchStatisticDto = _mapper.Map<MatchStatisticDto>(value.Statistic);

            return Ok(mappedValue);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFixture(UpdateFixtureDto updateFixtureDto)
        {
            var value = _mapper.Map<Fixture>(updateFixtureDto);
            _context.Fixtures.Update(value);
            await _context.SaveChangesAsync();
            return Ok("Fixture updated successfully");
        }
    }
}
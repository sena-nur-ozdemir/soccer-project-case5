using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerProjectCase5.WebApi.Context;
using SoccerProjectCase5.WebApi.Dtos.MatchStatisticDtos;
using SoccerProjectCase5.WebApi.Entities;

namespace SoccerProjectCase5.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchStatisticsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public MatchStatisticsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MatchStatisticList()
        {
            var values = await _context.MatchStatistics.ToListAsync();
            return Ok(values);
        }

        [HttpGet("GetMatchStatisticByFixture/{id}")]
        public async Task<IActionResult> GetMatchStatisticByFixture(int id)
        {
            var value = await _context.MatchStatistics.FirstOrDefaultAsync(x => x.FixtureId == id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchStatistic(int id)
        {
            var value = await _context.MatchStatistics.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchStatistic(CreateMatchStatisticDto createMatchStatisticDto)
        {
            var value = _mapper.Map<MatchStatistic>(createMatchStatisticDto);
            await _context.MatchStatistics.AddAsync(value);
            await _context.SaveChangesAsync();
            return Ok("Match statistic created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMatchStatistic(UpdateMatchStatisticDto updateMatchStatisticDto)
        {
            var value = _mapper.Map<MatchStatistic>(updateMatchStatisticDto);
            _context.MatchStatistics.Update(value);
            await _context.SaveChangesAsync();
            return Ok("Match statistic updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchStatistic(int id)
        {
            var value = await _context.MatchStatistics.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.MatchStatistics.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Match statistic deleted successfully.");
        }
    }
}
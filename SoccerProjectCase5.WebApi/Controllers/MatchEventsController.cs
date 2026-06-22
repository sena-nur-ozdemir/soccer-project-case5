using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerProjectCase5.WebApi.Context;
using SoccerProjectCase5.WebApi.Dtos.MatchEventDtos;
using SoccerProjectCase5.WebApi.Entities;

namespace SoccerProjectCase5.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchEventsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public MatchEventsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MatchEventList()
        {
            var values = await _context.MatchEvents.ToListAsync();
            return Ok(values);
        }

        [HttpGet("GetMatchEventsByFixture/{id}")]
        public async Task<IActionResult> GetMatchEventsByFixture(int id)
        {
            var values = await _context.MatchEvents.Where(x => x.FixtureId == id).OrderBy(x => x.Minute).ToListAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchEvent(CreateMatchEventDto createMatchEventDto)
        {
            var value = _mapper.Map<MatchEvent>(createMatchEventDto);
            await _context.MatchEvents.AddAsync(value);
            await _context.SaveChangesAsync();
            return Ok("Match event created successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchEvent(int id)
        {
            var value = await _context.MatchEvents.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.MatchEvents.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Match event deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchEvent(int id)
        {
            var value = await _context.MatchEvents.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMatchEvent(UpdateMatchEventDto updateMatchEventDto)
        {
            var value = _mapper.Map<MatchEvent>(updateMatchEventDto);
            _context.MatchEvents.Update(value);
            await _context.SaveChangesAsync();
            return Ok("Match event updated successfully");
        }
    }
}
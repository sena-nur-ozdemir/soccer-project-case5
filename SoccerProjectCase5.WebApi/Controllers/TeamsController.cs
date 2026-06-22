using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerProjectCase5.WebApi.Context;
using SoccerProjectCase5.WebApi.Dtos.TeamDtos;
using SoccerProjectCase5.WebApi.Entities;

namespace SoccerProjectCase5.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public TeamsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> TeamList()
        {
            var values = await _context.Teams.ToListAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam(CreateTeamDto createTeamDto)
        {
            var team = _mapper.Map<Team>(createTeamDto);
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return Ok("Team added successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var value = await _context.Teams.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.Teams.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Team deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var value = await _context.Teams.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
            return Ok("Team updated successfully");
        }
    }
}
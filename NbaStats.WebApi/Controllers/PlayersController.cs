using Microsoft.AspNetCore.Mvc;
using NbaStats.BLL.DTOs;
using NbaStats.BLL.Interfaces;
using NbaStats.BLL.Mappers;
using NbaStats.Mappers;

namespace NbaStats.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService playerService;
    private readonly ITeamService teamService;

    public PlayersController(IPlayerService playerService , ITeamService teamService)
    {
        this.playerService = playerService;
        this.teamService = teamService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerDto>>> GetAll()
    {
        var players = await playerService.GetAllAsync();
        return Ok(players.Select(p => p.ToDto()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerDto>> GetById(int id)
    {
        var player = await playerService.GetByIdAsync(id);
        if (player == null)
            return NotFound();
            
        return Ok(player.ToDto());
    }

    [HttpPost]
     public async Task<ActionResult<PlayerDto>> Create(PlayerCreateDto createDto)
     {
         if(!ModelState.IsValid)
             return BadRequest(ModelState);
         var team = await teamService.GetTeamByNameAsync(createDto.TeamName);
         var player = await playerService.AddAsync(PlayerCreateDtoMapper.ToEntity(createDto, team));
            if (player == null)
                return BadRequest("Failed to create player");
         return CreatedAtAction(nameof(GetById), new { id = player.PlayerId }, createDto);
     }
}
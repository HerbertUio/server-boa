using System.ComponentModel.DataAnnotations;
using Application.Services;
using Domain.Dtos.TicketDtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController: ControllerBase
{
    private readonly TicketService _ticketService;
    public TicketController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }
     [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto dto)
    {
        try
        {
            var newTicket = await _ticketService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTicketById), new { id = newTicket.Id }, newTicket);
        }
        catch (ValidationException ex)
        {
            
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        try
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            return Ok(ticket); 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        try
        {
            var tickets = await _ticketService.GetAllAsync();
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeTicketStatus(int id, [FromBody] ChangeStatusDto dto)
    {
        try
        {
            var updatedTicket = await _ticketService.ChangeStatusAsync(id, dto);
            return Ok(updatedTicket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        try
        {
            var success = await _ticketService.DeleteAsync(id);
            return NoContent(); 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpPost("{id}/comments")]
    public async Task<IActionResult> AddComment(int id, [FromBody] AddCommentDto dto)
    {
        try
        {
            var updatedTicket = await _ticketService.AddCommentAsync(id, dto);
            return Ok(updatedTicket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpPut("{id}/assign")]
    public async Task<IActionResult> AssignTicket(int id, [FromBody] AssignTicketDto dto)
    {
        try
        {
            var updatedTicket = await _ticketService.AssignTicketAsync(id, dto);
            return Ok(updatedTicket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpPost("merge")]
    public async Task<IActionResult> MergeTickets([FromBody] MergeTicketsDto dto)
    {
        try
        {
            var principalTicket = await _ticketService.MergeTicketsAsync(dto);
            return Ok(principalTicket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpPost("unmerge")]
    public async Task<IActionResult> UnmergeTickets([FromBody] UnmergeTicketsDto dto)
    {
        try
        {
            var principalTicket = await _ticketService.UnmergeTicketsAsync(dto);
            return Ok(principalTicket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpGet("{id}/comments")]
    public async Task<IActionResult> GetCommentsForTicket(int id)
    {
        try
        {
            var comments = await _ticketService.GetCommentsByTicketIdAsync(id);
            return Ok(comments);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(int id, [FromBody] UpdateTicketDto dto)
    {
        try
        {
            var updatedTicket = await _ticketService.UpdateAsync(id, dto);
            return Ok(updatedTicket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpGet("{id}/children")]
    public async Task<IActionResult> GetChildTickets(int id)
    {
        try
        {
            var childTickets = await _ticketService.GetChildTicketsAsync(id);
            return Ok(childTickets);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}
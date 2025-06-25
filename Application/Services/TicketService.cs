using Application.UseCases.TicketCases;
using Domain.Dtos.TicketDtos;
using Domain.Enums.TicketEnums;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services;

public class TicketService
{
    private readonly CreateTicketCase _createTicketCase;
    private readonly ChangePriorityCase _changePriorityCase;
    private readonly ChangeStatusCase _changeStatusCase;
    private readonly UpdateTicketCase _updateTicketCase;
    private readonly GetByIdTicketCase _getByIdTicketCase;
    private readonly GetAllTicketsCase _getAllTicketsCase;
    private readonly DeleteTicketCase _deleteTicketCase;
    private readonly MergeTicketsCase _mergeTicketsCase;
    private readonly UnmergeTicketsCase _unmergeTicketsCase;
    private readonly AddCommentCase _addCommentCase;
    private readonly AssignTicketCase _assignTicketCase;
    private readonly GetCommentsByTicketIdCase _getCommentsByTicketIdCase;
    private readonly GetChildTicketsCase _getChildTicketsCase;
    
    public TicketService(
        CreateTicketCase createTicketCase,
        ChangePriorityCase changePriorityCase,
        ChangeStatusCase changeStatusCase,
        UpdateTicketCase updateTicketCase,
        GetByIdTicketCase getByIdTicketCase,
        GetAllTicketsCase getAllTicketsCase,
        DeleteTicketCase deleteTicketCase,
        MergeTicketsCase mergeTicketsCase,
        UnmergeTicketsCase unmergeTicketsCase,
        AddCommentCase addCommentCase,
        AssignTicketCase assignTicketCase,
        GetCommentsByTicketIdCase getCommentsByTicketIdCase,
        GetChildTicketsCase getChildTicketsCase)
    {
        _createTicketCase = createTicketCase;
        _changePriorityCase = changePriorityCase;
        _changeStatusCase = changeStatusCase;
        _updateTicketCase = updateTicketCase;
        _getByIdTicketCase = getByIdTicketCase;
        _getAllTicketsCase = getAllTicketsCase;
        _deleteTicketCase = deleteTicketCase;
        _mergeTicketsCase = mergeTicketsCase;
        _unmergeTicketsCase = unmergeTicketsCase;
        _addCommentCase = addCommentCase;
        _assignTicketCase = assignTicketCase;
        _getCommentsByTicketIdCase = getCommentsByTicketIdCase;
        _getChildTicketsCase = getChildTicketsCase;
    }

    public async Task<TicketModel> CreateAsync(CreateTicketDto dto)
    {
        var result = await _createTicketCase.ExecuteAsync(dto);
        return result;
    }
    public async Task<TicketModel> ChangePriorityAsync(int ticketId, ChangePriorityDto dto)
    {
        var result = await _changePriorityCase.ExecuteAsync(ticketId, dto);
        return result;
    }
    public async Task<TicketModel> ChangeStatusAsync(int ticketId, ChangeStatusDto dto)
    {
        var result = await _changeStatusCase.ExecuteAsync(ticketId, dto);
        return result;
    }
    public async Task<TicketModel> UpdateAsync(int id, UpdateTicketDto updateTicketDto)
    {
        var result = await _updateTicketCase.ExecuteAsync(id, updateTicketDto);
        return result;
    }
    public async Task<TicketModel> GetByIdAsync(int id)
    {
        var result = await _getByIdTicketCase.ExecuteAsync(id);
        return result;
    }
    public async Task<IEnumerable<TicketModel>> GetAllAsync()
    {
        var result = await _getAllTicketsCase.ExecuteAsync();
        return result;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _deleteTicketCase.ExecuteAsync(id);
        return result;
    }
    public async Task<TicketModel> MergeTicketsAsync(MergeTicketsDto dto)
    {
        var result = await _mergeTicketsCase.ExecuteAsync(dto);
        return result;
    }
    public async Task<TicketModel> UnmergeTicketsAsync(UnmergeTicketsDto dto)
    {
        var result = await _unmergeTicketsCase.ExecuteAsync(dto);
        return result;
    }
    public async Task<TicketModel> AddCommentAsync(int ticketId, AddCommentDto dto)
    {
        var result = await _addCommentCase.ExecuteAsync(ticketId, dto);
        return result;
    }
    
    public async Task<TicketModel> AssignTicketAsync(int ticketId, AssignTicketDto dto)
    {
        return await _assignTicketCase.ExecuteAsync(ticketId, dto);
    }
    
    
    public async Task<IEnumerable<CommentModel>> GetCommentsByTicketIdAsync(int ticketId)
    {
        return await _getCommentsByTicketIdCase.ExecuteAsync(ticketId);
    }
    
    public async Task<IEnumerable<TicketModel>> GetChildTicketsAsync(int parentTicketId)
    {
        return await _getChildTicketsCase.ExecuteAsync(parentTicketId);
    }
}
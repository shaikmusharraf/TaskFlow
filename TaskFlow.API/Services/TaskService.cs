using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Entities;

namespace TaskFlow.API.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskResponseDto>> GetAllAsync()
    {
        return await _context.TaskItems
            .AsNoTracking()
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                AssignedToUserId = t.AssignedToUserId,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<TaskResponseDto?> GetByIdAsync(int id)
    {
        return await _context.TaskItems
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                AssignedToUserId = t.AssignedToUserId,
                CreatedAt = t.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto dto)
    {
        if (dto.AssignedToUserId.HasValue)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.Id == dto.AssignedToUserId.Value);

            if (!userExists)
            {
                throw new ArgumentException(
                    "The assigned user does not exist.");
            }
        }

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            AssignedToUserId = dto.AssignedToUserId,
            CreatedAt = DateTime.UtcNow
        };

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            AssignedToUserId = task.AssignedToUserId,
            CreatedAt = task.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task is null)
        {
            return false;
        }

        if (dto.AssignedToUserId.HasValue)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.Id == dto.AssignedToUserId.Value);

            if (!userExists)
            {
                throw new ArgumentException(
                    "The assigned user does not exist.");
            }
        }

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.Priority = dto.Priority;
        task.DueDate = dto.DueDate;
        task.AssignedToUserId = dto.AssignedToUserId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task is null)
        {
            return false;
        }

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs;

public class UpdateTaskDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [RegularExpression("^(Pending|InProgress|Completed)$")]
    public string Status { get; set; } = "Pending";

    [Required]
    [RegularExpression("^(Low|Medium|High)$")]
    public string Priority { get; set; } = "Medium";

    public DateTime? DueDate { get; set; }

    public int? AssignedToUserId { get; set; }
}
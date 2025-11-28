using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Beidar.Core.Enums;

namespace Beidar.Core.Models;

public class Expense
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public ExpenseCategory Category { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string? Notes { get; set; }
}

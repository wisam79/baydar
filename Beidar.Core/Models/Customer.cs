using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beidar.Core.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPurchases { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Debt { get; set; }

    public DateTime LastVisit { get; set; }

    public string? Notes { get; set; }

    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}

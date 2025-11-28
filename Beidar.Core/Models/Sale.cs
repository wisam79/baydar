using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Beidar.Core.Enums;

namespace Beidar.Core.Models;

public class Sale
{
    [Key]
    public int Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime SaleDate { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal VAT { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public SaleStatus Status { get; set; }

    public string? CustomerName { get; set; }

    public int? CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }

    public string? Notes { get; set; }

    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
}

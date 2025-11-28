using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beidar.Core.Models;

public class AppSettings
{
    [Key]
    public int Id { get; set; }

    public string StoreName { get; set; } = "My Store";

    public string? StorePhone { get; set; }

    public string? StoreAddress { get; set; }

    public string? StoreLogo { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal VATRate { get; set; } = 0;

    public string Currency { get; set; } = "IQD";

    public string? ReceiptFooter { get; set; }

    public string AccentColor { get; set; } = "#00C896";

    public bool CompactMode { get; set; } = false;

    public string FontSize { get; set; } = "Normal";

    public bool SoundEnabled { get; set; } = true;

    public int LowStockThreshold { get; set; } = 10;

    public bool AllowNegativeStock { get; set; } = false;

    public bool QuickSell { get; set; } = false;

    public bool AutoPrint { get; set; } = false;

    public string? AdminPin { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DailySalesTarget { get; set; }

    public string? GeminiApiKey { get; set; }
}

namespace EWallet.Api.Currencies.EndPoints;

public record CurrencyDto(
    [Required] [StringLength(100)] string Name,
    [Required] [StringLength(100)] string Code,
    [Required] decimal Ratio);
using System.Security.Cryptography.X509Certificates;

namespace EWallet.Api.Common.Dto;

public record WalletDto(decimal Balance, Guid CurrencyId);
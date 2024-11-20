namespace EWallet.Api.Common.Dto;

public abstract record WalletDto(decimal Balance, CurrencyId CurrencyId);
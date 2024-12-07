namespace EWallet.Api.Common;

public abstract class StaticData
{
    public static class ConnectionStrings
    {
        public const string Default = "Default";
    }

    public static class TableName
    {
        public const string Wallets = "Wallets";
        public const string Currencies = "Currencies";
        public const string Transactions = "Transactions";
    }

    public static class WalletDbContextDefaultColumnType
    {
        public const string Decimal = "DECIMAL(18, 2)";
        public const string Nvarchar36 = "NVARCHAR(36)";
    }

    public static class ErrorMessages
    {
        public const string DuplicateInput = "Oops! It looks like there's already an entry with the same 'Code' and 'Name'. Please try again with different values.";
    }
}
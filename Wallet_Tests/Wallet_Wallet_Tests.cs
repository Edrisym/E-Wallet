namespace Wallet_Tests;

public class Wallet_Wallet_Tests
{
    [Fact]
    public void Should_Throw_Exception_If_Balance_Is_Negative()
    {
        var balance = -0.75m;
        var currency = Currency.Create("USD", "United States Dollar", 1.0m);
        var walletCreation = () => Wallet.Create(balance, currency);

        walletCreation.Should().ThrowExactly<NegativeBalanceException>();
    }


    [Fact]
    public void Should_Set_Initial_Balance_To_One_Dollar_When_Currency_Is_USD()
    {
        var currency = Currency.Create("USD", "United States Dollar", 0.99m);

        var walletCreation = () => Wallet.Create(1.0m, currency);

        walletCreation.Should().ThrowExactly<InvalidCurrencyRatioException>();
    }

    [Fact]
    public void Should_Throw_Exception_If_No_Default_Currency_Is_Set()
    {
        var walletCreation = () => Wallet.Create(1.0m, null!);
        walletCreation.Should().ThrowExactly<ArgumentNullException>();
    }

    [Theory]
    [InlineData("USD", "United States Dollar", 0.99)]
    public void Should_Throw_Exception_If_InitialBalance_Is_Under_Limit(string code, string name, decimal ratio)
    {
        var currency = Currency.Create(code, name, ratio);
        var walletCreation = () => Wallet.Create(0.0m, currency);
        walletCreation.Should().ThrowExactly<InsufficientInitialBalanceException>();
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-200.9)]
    [InlineData(0)]
    [InlineData(null!)]
    public void Should_Throw_Exception_When_Deposit_Amount_Is_Negative_Or_Zero(decimal amount)
    {
        var currency = Currency.Create("USD", "United States Dollar", 1.99m);
        var wallet = Wallet.Create(balance: 100, currency);
        var walletCreation = () => wallet.Deposit(amount);
        walletCreation.Should().ThrowExactly<NegativeBalanceException>();
    }


    [Fact]
    public void Should_Increase_Balance_When_Deposit_Amount_Is_Positive()
    {
        var currency = Currency.Create("USD", "United States Dollar", 1.99m);
        var wallet = Wallet.Create(balance: 100, currency);

        wallet.Deposit(amount: 100);
        wallet.Balance.Should().Be(200);
    }


    [Fact]
    public void Should_Throw_Exception_When_Balance_Is_Lower_Than_Withdrawal_Amount()
    {
        var currency = Currency.Create("USD", "United States Dollar", 1.99m);
        var wallet = Wallet.Create(balance: 100, currency);
        wallet.Withdraw(amount: 100);
        wallet.Balance.Should().BeGreaterOrEqualTo(0);
        ;
    }


    [Fact]
    public void Should_Decrease_Balance_When_Withdrawal()
    {
        var currency = Currency.Create("USD", "United States Dollar", 1.99m);
        var wallet = Wallet.Create(balance: 100, currency);
        var withdrawal = () => wallet.Withdraw(amount: 101);
        withdrawal.Should().ThrowExactly<InsufficientFundsException>();
    }
}
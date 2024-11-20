
namespace Wallet_Tests;

public class WalletTests
{
    private static Currency CreateCurrency(string code = "USD", string name = "United States Dollar",
        decimal ratio = 1.99m) => Currency.Create(code, name, ratio);

    private static Wallet CreateWallet(decimal balance, decimal ratio = 1.99m)
    {
        var currency = CreateCurrency(ratio: ratio);
        return Wallet.Create(balance, currency);
    }

    [Fact]
    public void Should_Throw_Exception_If_Balance_Is_Negative()
    {
        var walletCreation = () => CreateWallet(balance: -0.75m, ratio: 1.0m);
        walletCreation.Should().ThrowExactly<NegativeBalanceException>();
    }

    [Fact]
    public void Should_Set_Initial_Balance_To_One_Dollar_When_Currency_Is_USD()
    {
        var walletCreation = () => CreateWallet(balance: 1.0m, ratio: 0.99m);
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
        var currency = CreateCurrency(code, name, ratio);
        var walletCreation = () => Wallet.Create(0.0m, currency);
        walletCreation.Should().ThrowExactly<InsufficientInitialBalanceException>();
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(0)]
    public void Should_Throw_Exception_When_Deposit_Amount_Is_Negative_Or_Zero(decimal amount)
    {
        var wallet = CreateWallet(balance: 100);
        var depositAction = () => wallet.Deposit(amount);
        depositAction.Should().ThrowExactly<NegativeBalanceException>();
    }

    [Fact]
    public void Should_Increase_Balance_When_Deposit_Amount_Is_Positive()
    {
        var wallet = CreateWallet(balance: 100);
        wallet.Deposit(amount: 100);
        wallet.Balance.Should().Be(200);
    }

    [Fact]
    public void Should_Throw_Exception_When_Balance_Is_Lower_Than_Withdrawal_Amount()
    {
        var wallet = CreateWallet(balance: 100);
        wallet.Withdraw(amount: 100);
        wallet.Balance.Should().BeGreaterOrEqualTo(0);
    }

    [Fact]
    public void Should_Decrease_Balance_When_Withdrawal_Amount_Exceeds_Balance()
    {
        var wallet = CreateWallet(balance: 100);
        var withdrawal = () => wallet.Withdraw(amount: 101);
        withdrawal.Should().ThrowExactly<InsufficientFundsException>();
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(0)]
    [InlineData(-113)]
    public void Should_Throw_Exception_When_Withdrawal_Amount_Is_Zero_Or_Negative(decimal amount)
    {
        var wallet = CreateWallet(balance: 100);
        var withdrawal = () => wallet.Withdraw(amount);
        withdrawal.Should().ThrowExactly<InvalidDataException>();
    }

    [Fact]
    public void Should_Set_Status_To_UnderReview_When_Creating_New_Wallet()
    {
        var wallet = CreateWallet(balance: 100);
        wallet.Status.Should().Be(WalletStatus.UnderReview);
    }

    [Fact]
    public void Should_Change_Status_When_Changing_Wallet_Status()
    {
        var wallet = CreateWallet(balance: 100);
        wallet.Status.Should().Be(WalletStatus.UnderReview);
    }

    [Fact]
    public void Should_Change_Status_When_Valid_Transition()
    {
        var wallet = CreateWallet(balance: 100);
        wallet.Status.Should().Be(WalletStatus.UnderReview);

        wallet.PendActivation();

        wallet.Status.Should().Be(WalletStatus.PendingActivation);
        wallet.StatusId.Should().Be((int)WalletStatus.PendingActivation);

        wallet.Activate();

        wallet.Status.Should().Be(WalletStatus.Active);
        wallet.StatusId.Should().Be((int)WalletStatus.Active);

        wallet.Suspend();

        wallet.Status.Should().Be(WalletStatus.Suspended);
        wallet.StatusId.Should().Be((int)WalletStatus.Suspended);

        wallet.Close();

        wallet.Status.Should().Be(WalletStatus.Closed);
        wallet.StatusId.Should().Be((int)WalletStatus.Closed);
    }

    [Fact]
    public void Should_Throw_Exception_When_Invalid_Transition()
    {
        var wallet = CreateWallet(balance: 100);
        var changeStatusOpt = () => wallet.Activate();
        changeStatusOpt.Should().ThrowExactly<InvalidOperationException>();
    }
}
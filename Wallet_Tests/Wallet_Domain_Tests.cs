namespace Wallet_Tests;

public class Wallet_Domain_Tests
{
    //decimal.FromOACurrency()
    // TODO -- testing steps
    //Arrange & Act 
    //Assert

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

        var walletCreation = () => Wallet.Create(0.1m, currency);

        walletCreation.Should().ThrowExactly<InvalidCurrencyRatioException>();
    }

    [Fact]
    public void Should_Throw_Exception_If_No_Default_Is_Set()
    {
        var walletCreation = () => Wallet.Create(0.1m, null!);
        walletCreation.Should().ThrowExactly<ArgumentNullException>();
    }
}
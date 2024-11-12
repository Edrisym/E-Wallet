using FluentAssertions;

namespace Wallet_Tests;

public class Wallet_Domain_Tests
{
    // TODO -- testing steps
    //Arrange & Act 
    //assert

    [Fact]
    public void Should_Throw_Exception_If_Balance_Is_Negative()
    {
        var balance = -0.75m;
        var guidId = Guid.NewGuid();
        
        var walletCreation = () => new Wallet(guidId, balance);
        
        walletCreation.Should().ThrowExactly<NegativeBalanceException>();
    }
}

public class Wallet
{
    public Wallet()
    {
    }

    public Guid Id { get; private set; }
    public decimal Balance { get; private set; }

    public Wallet(Guid id, decimal balance)
    {
        if (decimal.IsNegative(balance))
        {
            NegativeBalanceException.Throw(balance);
        }

        Id = id;
        Balance = balance;
    }
}
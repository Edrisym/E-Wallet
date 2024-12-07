using Microsoft.EntityFrameworkCore;
using Wallet_Tests;

public class Wallet_CurrencyTests
{
    private readonly DbContextFixture _fixture;

    public Wallet_CurrencyTests()
    {
        _fixture = new DbContextFixture();
    }

    private static Currency CreateCurrency(string code = "USD", string name = "United States Dollar",
        decimal ratio = 1.99m)
    {
        return Currency.Create(code, name, ratio);
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(-100.5)]
    [InlineData(-0.01)]
    public void Should_Throw_Exception_If_Ratio_Is_Negative(decimal ratio)
    {
        var currencyCreation = () => CreateCurrency(ratio: ratio);
        currencyCreation.Should().ThrowExactly<InvalidCurrencyRatioException>();
    }

    [Fact]
    public void Should_Create_Currency_If_Ratio_Is_Valid()
    {
        var currency = CreateCurrency(ratio: 1.0m);
        currency.Code.Should().Be("USD");
        currency.Ratio.Should().Be(1.0m);
    }

    [Theory]
    [InlineData(null!, null!, 1.0)]
    [InlineData("", "", 1.0)]
    public void Should_Throw_Exception_If_Currency_Code_Is_Null_Or_Empty(string code, string name, decimal ratio)
    {
        var currencyCreation = () => CreateCurrency(code, name, ratio);
        currencyCreation.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Should_Have_Valid_Code_After_Creation()
    {
        var currency = CreateCurrency();
        currency.Code.Should().NotBeNullOrEmpty();
    }


    [Fact]
    public async Task InsertUser_ShouldAddUserToDatabase()
    {
        await using var context = _fixture.CreateDbContext();

        var currency = CreateCurrency();

        await context.Currencies.AddAsync(currency);
        await context.SaveChangesAsync();

        var dollar2 = CreateCurrency();
        var action = async () =>
        {
            await context.Currencies.AddAsync(dollar2);
            await context.SaveChangesAsync();
        };
        await action.Should()
            .ThrowAsync<DbUpdateException>("because the email is unique and cannot be duplicated")
            .WithMessage("*unique*");
    }
}
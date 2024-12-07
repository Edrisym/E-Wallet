using System.Net;
using System.Net.Http.Json;
using EWallet.Api.Wallets.EndPoints;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;

namespace Integrations.Wallet;

public class WalletEndpointTests
{
    private readonly WalletDbContext _db;
    private readonly IntegrationFixture _fixture;

    public WalletEndpointTests(IntegrationFixture fixture)
    {
        _db = fixture.CreateContext();
        _fixture = fixture;
    }
    
    public async Task TestGetApi()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/wallets");

        // Act
        var response = await _fixture.SendRequestAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Equal("Test response", responseContent);
    }

    public async Task Ensure_Wallet_Created_Successfully(WalletDto request)
    {
        // Arrange
        // Act
        // var response = await _factory.PostAsJsonAsync("/api/v1/wallets/", request);
        // Assert
        // response.Should().BeSuccessful();
        // response.Should().NotBeNull();
    }

    public async Task Ensure_Wallet_Created_With_All_Fields_Successfully(WalletDto request)
    {
        // Arrange
        var wallet = new WalletDto(10m, Guid.NewGuid());

        // Act
        // var response = await _factory.CreateClient().PostAsJsonAsync("/api/v1/wallets/", request);

        // Assert
        // response.Should().BeEquivalentTo(request);
    }
}
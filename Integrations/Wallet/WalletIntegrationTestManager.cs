using EWallet.Api.Wallets.EndPoints;

namespace Integrations.Wallet;

public class WalletIntegrationTestManager : IClassFixture<IntegrationFixture>
{
    private readonly IntegrationFixture _factory;
    private readonly WalletEndpointTests _endpointTests;
    
    public WalletIntegrationTestManager(IntegrationFixture factory)
    {
        _factory = factory;
        _endpointTests = new WalletEndpointTests(_factory); 
    }


    [Fact]
    public async Task TestGetApi()
    {
        await _endpointTests.TestGetApi();
    }
    // [Fact]
    // public async Task Ensure_Wallet_Create_Successfully()
    // {
    //     // Test data
    //     var wallet = new WalletDto(10m, Guid.NewGuid());
    //
    //     // Use the endpoint tests
    //     await _endpointTests.Ensure_Wallet_Created_Successfully(wallet);
    // }
}
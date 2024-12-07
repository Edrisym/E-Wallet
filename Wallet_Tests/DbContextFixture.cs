using Microsoft.EntityFrameworkCore;

namespace Wallet_Tests;

public class DbContextFixture
{
    public WalletDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<WalletDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new WalletDbContext(options);
    }
}
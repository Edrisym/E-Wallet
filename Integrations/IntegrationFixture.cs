using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestContainers.Container.Abstractions.Hosting;
using TestContainers.Container.Database.Hosting;
using TestContainers.Container.Database.MsSql;

namespace Integrations
{
    public class IntegrationFixture : IAsyncDisposable
    {
        private readonly MsSqlContainer _container;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IntegrationFixture()
        {
            _container = new ContainerBuilder<MsSqlContainer>()
                .ConfigureDatabaseConfiguration("sa", "reallyStrongPwd123!", "WalletDb")
                .Build();

            _container.StartAsync().GetAwaiter().GetResult();
        }

        public WalletDbContext CreateContext()
        {
            var connectionString = _container.GetConnectionString();

            var dbContext = new WalletDbContext(
                new DbContextOptionsBuilder<WalletDbContext>()
                    .UseSqlServer(connectionString)
                    .Options);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await _container.StopAsync();
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            return await _client.SendAsync(request);
        }

        public void Dispose()
        {
            _container.StopAsync().GetAwaiter().GetResult();
        }
    }
}
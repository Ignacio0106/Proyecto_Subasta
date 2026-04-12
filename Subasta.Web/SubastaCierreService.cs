using Microsoft.AspNetCore.SignalR;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Web.Hubs;

namespace Subasta.Web
{
    public class SubastaCierreService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<SubastaHub> _hubContext;

        public SubastaCierreService(IServiceScopeFactory scopeFactory, IHubContext<SubastaHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var serviceSubasta = scope.ServiceProvider.GetRequiredService<IServiceSubasta>();

                await serviceSubasta.CerrarSubastasVencidasAsync(); // método que debes crear

                // Notificar por SignalR a cada subasta cerrada
                // (el service puede retornar la lista de IDs cerrados)

                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken); // revisa cada 15 seg
            }
        }
    }
}

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

                var idsCerradas = await serviceSubasta.CerrarSubastasVencidasAsync();

                foreach (var idSubasta in idsCerradas)
                {
                    var resultado = await serviceSubasta.ObtenerResultadoAsync(idSubasta);

                    await _hubContext.Clients
                        .Group($"Subasta-{idSubasta}")
                        .SendAsync("SubastaCerrada", new
                        {
                            hayGanador = resultado != null && resultado.IdUsuarioGanador > 0,
                            ganador = resultado?.IdUsuarioGanadorNavigation?.NombreCompleto ?? string.Empty,
                            montoFinal = resultado?.MontoFinal ?? 0
                        }, stoppingToken);
                }

                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken); // revisa cada 15 seg
            }
        }
    }
}

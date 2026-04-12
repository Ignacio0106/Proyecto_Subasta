using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Subasta.Infraestructure.Data;

namespace Subasta.Web.Hubs
{
    public class SubastaHub : Hub
    {
        private readonly SubastaContext _context;

        public SubastaHub(SubastaContext context)
        {
            _context = context;
        }
        public async Task UnirseSubasta(string idSubasta)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Subasta-{idSubasta}");
        }

        public async Task SalirSubasta(string idSubasta)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Subasta-{idSubasta}");
        }

        public async Task ObtenerHistorial(int idSubasta)
        {
            try
            {
                var pujas = await _context.Puja
                    .Where(p => p.IdSubasta == idSubasta)
                    .OrderByDescending(p => p.FechaHora)
                    .Select(p => new {
                        nombreUsuario = p.IdUsuarioNavigation.NombreCompleto,
                        montoOfertado = p.MontoOfertado,
                        fechaHora = p.FechaHora
                    })
                    .ToListAsync();

                await Clients.Caller.SendAsync("HistorialInicial", pujas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR EN HUB: " + ex.Message);
            }
        }

        
    }
}
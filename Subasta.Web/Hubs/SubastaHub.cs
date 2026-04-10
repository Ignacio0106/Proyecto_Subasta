using Microsoft.AspNetCore.SignalR;

namespace Subasta.Web.Hubs
{
    public class SubastaHub : Hub
    {
        public async Task UnirseSubasta(string idSubasta)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Subasta-{idSubasta}");
        }

        public async Task SalirSubasta(string idSubasta)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Subasta-{idSubasta}");
        }
    }
}
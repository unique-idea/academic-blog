using Microsoft.AspNetCore.SignalR;

namespace Academic_Blog_App.Services.ClientAjax
{
    public class CenterHub : Hub
    {
        private readonly IHubContext<CenterHub> _hubContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CenterHub(IHubContext<CenterHub> hubContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
        }

        public async Task TrackPost(string postId)
        {
            SettingHub();
            await _hubContext.Clients.All.SendAsync("TrackPost", postId);
        }

        private void SettingHub()
        {
            _httpContextAccessor.HttpContext!.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            _httpContextAccessor.HttpContext!.Response.Headers["Pragma"] = "no-cache";
            _httpContextAccessor.HttpContext!.Response.Headers["Expires"] = "0";
        }
    }
}

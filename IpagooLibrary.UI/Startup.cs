using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IpagooLibrary.UI.Startup))]
namespace IpagooLibrary.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}

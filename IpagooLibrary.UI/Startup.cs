using IpagooLibrary.UI.Hubs.PipelineModules;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IpagooLibrary.UI.Startup))]
namespace IpagooLibrary.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());

            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}

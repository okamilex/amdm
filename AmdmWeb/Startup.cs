using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmdmWeb.Startup))]
namespace AmdmWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

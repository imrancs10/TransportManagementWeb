using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransportManagementWeb.Startup))]
namespace TransportManagementWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}

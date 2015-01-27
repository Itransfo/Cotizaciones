using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cotizaciones.Startup))]
namespace Cotizaciones
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

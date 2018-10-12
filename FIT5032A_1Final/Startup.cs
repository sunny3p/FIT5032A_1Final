using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIT5032A_1Final.Startup))]
namespace FIT5032A_1Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

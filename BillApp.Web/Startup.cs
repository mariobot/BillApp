using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BillApp.Web.Startup))]
namespace BillApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

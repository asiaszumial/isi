using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ISIProject.Startup))]
namespace ISIProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

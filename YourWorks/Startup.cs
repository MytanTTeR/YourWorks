using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YourWorks.Startup))]
namespace YourWorks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

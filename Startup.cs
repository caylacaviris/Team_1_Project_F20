using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Team_1_Project.Startup))]
namespace Team_1_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

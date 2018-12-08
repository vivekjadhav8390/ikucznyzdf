using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InstituteProject.Startup))]
namespace InstituteProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

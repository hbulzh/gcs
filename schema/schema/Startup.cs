using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(schema.Startup))]
namespace schema
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

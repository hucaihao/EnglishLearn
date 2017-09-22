using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnglishLearn.Startup))]
namespace EnglishLearn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

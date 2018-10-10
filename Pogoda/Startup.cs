using Microsoft.Owin;
using Owin;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(Pogoda.Startup))]
namespace Pogoda
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app); 
            
        }
    }
}

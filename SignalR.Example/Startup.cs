using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

namespace SignalR.Example
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HubConfiguration config = new HubConfiguration();
            //config.EnableCrossDomain = true;
            config.EnableDetailedErrors = true;
            app.MapSignalR();
        }
    }
}
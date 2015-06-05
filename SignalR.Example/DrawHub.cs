using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Example
{
    public class DrawHub : Hub
    {
        public void SetPosition(Point p)
        {
            Clients.Others.ChangePosition(p);
        }

        public void SetDeltaPosition(Point p)
        {
            Clients.Others.SetDeltaPosition(p);
        }
    }


}
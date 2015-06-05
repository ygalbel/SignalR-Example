using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Example
{
    public class YoutubeHub : Hub
    {
        public void Play()
        {
            Clients.Others.Play();
        }

        public void Stop()
        {
            Clients.Others.Stop();
        }
    }


    public class Point 
    {
        public int x;

        public int y;
    }
}
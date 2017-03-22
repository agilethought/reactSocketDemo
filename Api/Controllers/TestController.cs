using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Api.Controllers
{
    using System.Configuration;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.WebSockets;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Cors;
    using System.Web.Script.Serialization;
    using System.Web.WebSockets;

    using Microsoft.AspNet.SignalR.Hubs;

    [EnableCors("*", "*", "*", SupportsCredentials = true)]
    public class TestController : ApiController
    {
        public HttpResponseMessage Get(string username)
        {
            HttpContext.Current.AcceptWebSocketRequest(ChatWebSocketSession);
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        private static async Task ChatWebSocketSession(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;
            var name = "mike";
            var json = NextName(name, out name);

            var i = 0;
            while (i < 10)
            {
                var b = Encoding.ASCII.GetBytes(json);
                var bytesToSend = new ArraySegment<byte>(b);
                await socket.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
                i++;
                json = NextName(name, out name);
                System.Threading.Thread.Sleep(3000);
            }
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "We are done", CancellationToken.None);
        }

        private static string NextName(string nameIn, out string name)
        {
            name = nameIn + "a";
            var x = new { name = name };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(x);
            return json;
        }
    }
}
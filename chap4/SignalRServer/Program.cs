// https://stackoverflow.com/questions/11140164/signalr-console-app-example
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Owin;
using SignalRServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: OwinStartup(typeof(Program.Startup))]
namespace SignalRServer
{
    class Program
    {
        static IDisposable SignalR;

        static void Main(string[] args)
        {
            string url = "http://127.0.0.1:8088";
            SignalR = WebApp.Start(url);

            Console.ReadKey();
        }

        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                //app.UseCors(Microsoft.Owin.Cors.CorsOptions CorsOptions.AllowAll);

                /*  CAMEL CASE & JSON DATE FORMATTING
                 use SignalRContractResolver from
                https://stackoverflow.com/questions/30005575/signalr-use-camel-case

                var settings = new JsonSerializerSettings()
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                };

                settings.ContractResolver = new SignalRContractResolver();
                var serializer = JsonSerializer.Create(settings);

               GlobalHost.DependencyResolver.Register(typeof(JsonSerializer),  () => serializer);                

                 */

                app.MapSignalR();
            }
        }

        [HubName("MyHub")]
        public class MyHub : Hub
        {
            public void Send(string name, string message)
            {
                Clients.All.addMessage(name, message);
            }
        }
    }
}

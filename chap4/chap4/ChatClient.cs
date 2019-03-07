using Microsoft.AspNet.SignalR.Client;
using System;

namespace chap4
{
    internal class ChatClient : IChatConnection
    {
        public event Action<string> Received;
        public event Action Closed;
        public event Action<Exception> Error;
        public IHubProxy myHub { get; set; }
        public IHubProxy Connect(string url, string hubName, string userName)
        {
            var connection = new HubConnection(url);
            myHub = connection.CreateHubProxy(hubName);
            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Error(task.Exception.GetBaseException());
                }
                else
                {
                    //Console.WriteLine("Connected");

                    myHub.On<string, string>("addMessage", (s1, s2) => {
                        //Console.WriteLine(s1 + ": " + s2);
                        Received(s1 + ": " + s2);
                    });
                }

            }).Wait();
            return myHub;
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
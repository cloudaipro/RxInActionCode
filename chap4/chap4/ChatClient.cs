using Microsoft.AspNet.SignalR.Client;
using System;
using System.Reactive.Linq;
using System.Windows.Threading;

namespace chap4
{
    internal class ChatClient : IChatConnection
    {
        public event Action<string> Received;
        public event Action Closed;
        public event Action<Exception> Error;
        public event Action Connected;

        public IHubProxy myHub { get; set; }

        public ChatClient() { }
        public ChatClient(Action connectedHandler)
        {
            Connected += connectedHandler;
        }


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

        public IChatConnection Connect(string url, string hubName, string userName, out IHubProxy hubObj)
        {
            var connection = new HubConnection(url);

            Observable.FromEvent<StateChange>(h => connection.StateChanged += h, h => connection.StateChanged -= h)
                      .ObserveOnDispatcher()
                      .Subscribe((s) =>
                      {
                          if (s.NewState == ConnectionState.Connected)
                              Connected?.Invoke();
                      });

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
            hubObj = myHub;
            return this;
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace chap4
{
    /// <summary>
    /// Interaction logic for MainWindowAndOffline.xaml
    /// </summary>
    public partial class MainWindowAndOffline : Window
    {
        //HubConnection connection;
        IHubProxy myHub;
        public MainWindowAndOffline()
        {
            InitializeComponent();

            //(new NumbersObservable(10)).Subscribe(chartCtl);
            IEnumerable<string> loadedMessage = LoadMessageFromDB();
            //Observable.FromEventPattern<EventArgs>(btnConnect, nameof(btnConnect.Click))
            //          .SubscribeOnDispatcher()
            //          .Subscribe(x => loadedMessage
            //                        .ToObservable()
            //                        .Concat(new ChatClient(() => btnConnect.IsEnabled = false)
            //                                    .Connect("http://127.0.0.1:8088/", "MyHub", Name.Text, out myHub)
            //                                    .ToObservable())
            //                        .Subscribe(chatCtl));

            Observable.FromEventPattern<EventArgs>(btnConnect, nameof(btnConnect.Click))
                      .SubscribeOnDispatcher()
                      .Subscribe(x =>new ChatClient(() => btnConnect.IsEnabled = false)
                                    .Connect("http://127.0.0.1:8088/", "MyHub", Name.Text, out myHub)
                                    .ToObservable()
                                    .StartWith(loadedMessage)
                                    .Subscribe(chatCtl));

            Observable.FromEvent<RoutedEventHandler, Tuple<object, RoutedEventArgs>>(
                        delegate (Action<Tuple<object, RoutedEventArgs>> rxHandle)
                        {
                            return (o, routedEventArgs) => rxHandle(Tuple.Create(o, routedEventArgs));
                        },
                        h => btnSend.Click += h,
                        h => btnSend.Click -= h)
                      .ObserveOnDispatcher()
                      .Subscribe(x => myHub.Invoke<string>("Send", Name.Text, message.Text));

            //Unit x;
        }


        public IEnumerable<string> LoadMessageFromDB()
        {
            yield return "Convert 20 mm to cm";
            yield return "20 mm = 20 ÷ 10 = 2 cm";
            yield return "How to perform metric conversion using the table method or shortcut method?";
        }


        //private void btnConnect_Click(object sender, RoutedEventArgs e)
        //{
        //    //V3
        //    Observable.Defer<string>(() =>
        //    {
        //        return new ChatClient().Connect("http://127.0.0.1:8088/", "MyHub", Name.Text, out myHub)
        //                               .ToObservable();
        //    })
        //    .Subscribe(chatCtl);


        //    //V2
        //   //new ChatClient().Connect("http://127.0.0.1:8088/", "MyHub", Name.Text, out myHub)
        //   //                .ToObservable()
        //   //                .Subscribe(chatCtl);

        //    //V1
        //    //var chatClient = new ChatClient();
        //    //myHub = chatClient.Connect("http://127.0.0.1:8088/", "MyHub", Name.Text);
        //    //IObservable<string> observableConnection =
        //    //    new ObservableConnection(chatClient);
        //    //var subscription = observableConnection.Subscribe(chatCtl);
        //}

        //private void BtnSend_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        myHub.Invoke<string>("Send", Name.Text, message.Text);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}

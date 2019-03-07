using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection connection;
        IHubProxy myHub;
        public MainWindow()
        {
            InitializeComponent();

            //(new NumbersObservable(10)).Subscribe(chartCtl);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            var chatClient = new ChatClient();
            myHub = chatClient.Connect("http://127.0.0.1:8088/", "MyHub", Name.Text);
            IObservable<string> observableConnection =
                new ObservableConnection(chatClient);
            var subscription = observableConnection.Subscribe(chatCtl);
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myHub.Invoke<string>("Send", Name.Text, message.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

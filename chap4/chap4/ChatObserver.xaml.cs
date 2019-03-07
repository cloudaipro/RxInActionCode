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
    /// Interaction logic for ChatObserver.xaml
    /// </summary>
    public partial class ChatObserver : ObserverCtl<string>
    {
        HubConnection connection;
        IHubProxy myHub;

        public ChatObserver()
        {
            InitializeComponent();
        }

        public override void OnCompleted()
        {
            MessageBox.Show("Chat END.");
        }

        public override void OnError(Exception error)
        {
            MessageBox.Show("Disconnect.");
        }

        public override void OnNext(string value)
        {
            Dispatcher.Invoke(() =>
            {
                chatContent.Items.Add(receiveMsg(value));
            });
        }

        private TextBlock receiveMsg(string message)
        {
            TextBlock obj = new TextBlock();
            obj.Text = message;
            obj.Foreground = Brushes.Blue;
            obj.HorizontalAlignment = HorizontalAlignment.Left;
            return obj;
        }
    }
}

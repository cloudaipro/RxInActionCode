using System;
using System.Collections.Generic;
using System.Linq;
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

namespace chap5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int iAmount = Int32.Parse(amount.Text);
            MagicalPrimeGenerator generator =  new MagicalPrimeGenerator();
            generator.startOff = Int32.Parse(offset.Text);
            foreach (int i in generator.Generate(iAmount))
                Console.WriteLine(DateTime.Now.ToString("mm:ss.fff") + "@" + i.ToString());
            //generator.Generate(iAmount).ToObservable<int>()
            //                       .Timestamp()
            //                       .Subscribe(x => Console.WriteLine(x.ToString()));
            var primes = await generator.GenerateAsync(iAmount);
            primes.ToObservable<int>()
                                        .Timestamp()
                                        .Subscribe(x => Console.WriteLine(x.ToString()));
        }
    }
}

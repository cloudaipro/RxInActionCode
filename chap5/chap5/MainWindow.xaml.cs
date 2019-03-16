using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MagicalPrimeGenerator generator = new MagicalPrimeGenerator();
            generator.GeneratePrimes(10)
                     .Timestamp()
                     .Subscribe(x => Console.WriteLine(x.ToString()));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //(new SearchEngineExample()).Search("ABC").Subscribe(x => Console.WriteLine(x));
            (new SearchEngineExample()).Search_ConcatingTasks("ABC").Subscribe(x => Console.WriteLine(x));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var svc = new PrimeCheckService();

            //var subscription = Observable.Range(2, 10)
            //                             .SelectMany((number) => svc.IsPrimeAsync(number),
            //                                         (number, isPrime) => new { number, isPrime})
            //                             .Where(x => x.isPrime)
            //                             .Select(x => x.number)
            //                             .Subscribe(x => Console.WriteLine(x));

            //var subscription = Observable.Range(2, 10)
            //                             .SelectMany((number) => svc.IsPrimeAsync(number),
            //                                         (number, isPrime) => new { number, isPrime })
            //                             .Where(x => x.isPrime)
            //                             .Select(x => x.number)
            //                             .Subscribe(x => Console.WriteLine(x));
            var subscription1 = Observable.Range(2, 10)
                             .SelectMany(async (number) => await svc.IsPrimeAsync(number))                             
                             .Where(x => x)
                             .Select(x => x)
                             .Subscribe(x => Console.WriteLine(x));

            //IObservable<int> primes =
            //    from number in Observable.Range(2, 10)
            //    from isPrime in svc.IsPrimeAsync(number)
            //    where isPrime
            //    select number;
            //primes.Subscribe(x => Console.WriteLine(x));


        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var svc = new PrimeCheckService();
            IObservable<int> primes =
                Observable.Range(2, 10)
                          .Select(async number => new { number, isPrime = await svc.IsPrimeAsync(number) })
                          .Concat()
                          .Where(x => x.isPrime)
                          .Select(x => x.number);
            primes.Subscribe(x => Console.WriteLine(x));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var updatesWebService = new UpdatesWebService();
            //var _subscribe = Observable.Interval(TimeSpan.FromSeconds(1))
            //                           .SelectMany(_ => updatesWebService.GetUpdatesAsync())                                       
            //                           .SelectMany(updates => updates)
            //                           .ObserveOnDispatcher()
            //                           .Subscribe(x => Console.WriteLine(x));

            var _subscribe = Observable.Interval(TimeSpan.FromSeconds(1))
                                       .Select(index  => updatesWebService.GetUpdatesAsync(index))
                                       .Concat()
                                       .SelectMany(x => x)
                                       .ObserveOnDispatcher()
                                       .Subscribe(x => Console.WriteLine(x));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chap5
{
    class UpdatesWebService
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public async Task<IEnumerable<string>> GetUpdatesAsync()
        {
            Console.WriteLine("GetUpdatesAsync was called");
            
            return await Task.Run(() => Enumerable.Range(1, 5)
                             .Select((x) => {
                                 Thread.Sleep(random.Next() % 1000);
                                 return String.Format("An Update {0}, {1}", x, DateTime.Now.ToString("mm:ss.fff")); }));
        }
        public async Task<IEnumerable<string>> GetUpdatesAsync(long index)
        {
            //Console.WriteLine(index + ": GetUpdatesAsync was called");

            return await Task.Run(() => Enumerable.Range(1, 5)
                             .Select((x) => {
                                 Thread.Sleep(random.Next() % 1000);
                                 return String.Format(index + ": An Update {0}, {1}", x, DateTime.Now.ToString("mm:ss.fff"));
                             }));
        }
    }
}

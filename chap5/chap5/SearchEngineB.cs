using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chap5
{
    class SearchEngineB : ISearchEngine
    {
        public async Task<IEnumerable<string>> SearchAsync(string term)
        {
            Console.WriteLine("SearchEngine B - SearchAsync()");
            await Task.Delay(1500);
            return new[] { "resultC", "resultD" };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chap5
{
    class SearchEngineA : ISearchEngine
    {
        public async Task<IEnumerable<string>> SearchAsync(string term)
        {
            Console.WriteLine("Searchine A - SearchAsync()");

            await Task.Delay(1500);
            return new[] { "resultA", "resultB" }.AsEnumerable();
        }
    }
}

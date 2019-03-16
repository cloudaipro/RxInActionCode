using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chap5
{
    interface ISearchEngine
    {
        Task<IEnumerable<string>> SearchAsync(string term);
    }
}

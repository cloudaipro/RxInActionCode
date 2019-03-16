using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace chap5
{
    class SearchEngineExample
    {
        public IObservable<string> Search(string term)
        {
            return Observable.Create<string>(async x =>
            {
                var searchEngineA = new SearchEngineA();
                var searchEngineB = new SearchEngineB();
                var resultA = await searchEngineA.SearchAsync(term);
                foreach(var result in resultA)
                {
                    x.OnNext(result);
                }
                var resultB = await searchEngineB.SearchAsync(term);
                foreach(var result in resultB)
                {
                    x.OnNext(result);
                }
                x.OnCompleted();
            });
        }

        public IObservable<string> Search_ConcatingTasks(string term)
        {
                var searchEngineA = new SearchEngineA();
                var searchEngineB = new SearchEngineB();
                IObservable<IEnumerable<string>> resultA = searchEngineA.SearchAsync(term).ToObservable();
                IObservable<IEnumerable<string>> resultB = searchEngineB.SearchAsync(term).ToObservable();

                return resultA
                        .Concat(resultB)
                        .SelectMany(results => results);
        }
    }
}

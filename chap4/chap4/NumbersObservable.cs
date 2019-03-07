using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace chap4
{
    public class NumbersObservable : IObservable<string>
    {
        private readonly int _amount;

        public NumbersObservable(int amount)
        {
            _amount = amount;
        }
        public IDisposable Subscribe(IObserver<string> observer)
        {
            for (int i = 0; i < _amount; i++)
                observer.OnNext(i.ToString());
            observer.OnCompleted();
            return Disposable.Empty;
        }
    }
}

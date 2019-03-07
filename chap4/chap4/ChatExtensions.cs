using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chap4
{
    public static class ChatExtensions
    {
        public static IObservable<string> ToObservable(this IChatConnection connection)
        {
            return Observable.Create<string>(
                observer =>
                {
                    Action<string> OnReceive = message =>
                    {
                        observer.OnNext(message);
                    };
                    connection.Received += OnReceive;

                    Action OnComplete = () =>
                    {
                        observer.OnCompleted();
                    };
                    connection.Closed += OnComplete;

                    Action<Exception> OnError = ex =>
                    {
                        observer.OnError(ex);
                    };
                    connection.Error += OnError;

                    return () =>
                    {
                        connection.Received -= OnReceive;
                        connection.Closed -= OnComplete;
                        connection.Error -= OnError;
                    };
                    //return Disposable.Create(() =>
                    //{
                    //    connection.Received -= OnReceive;
                    //    connection.Closed -= OnComplete;
                    //    connection.Error -= OnError;
                    //});
                });
        }
    }
}

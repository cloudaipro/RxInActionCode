using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace chap4
{
    class ObservableConnection : ObservableBase<string>
    {
        private readonly IChatConnection _ChatConnection;

        public ObservableConnection(IChatConnection chatConnection)
        {
            _ChatConnection = chatConnection;
        }

        protected override IDisposable SubscribeCore(IObserver<string> observer)
        {
            Action<string> received = message =>
            {
                observer.OnNext(message);
            };
            Action closed = () =>
            {
                observer.OnCompleted();
            };
            Action<Exception> error = ex =>
            {
                observer.OnError(ex);
            };
            _ChatConnection.Received += received;
            _ChatConnection.Closed += closed;
            _ChatConnection.Error += error;

            return Disposable.Create(() =>
            {
                _ChatConnection.Received -= received;
                _ChatConnection.Closed -= closed;
                _ChatConnection.Error -= error;
            });
        }
    }
}

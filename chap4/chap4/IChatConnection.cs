using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chap4
{
    interface IChatConnection
    {
        event Action<string> Received;
        event Action Closed;
        event Action<Exception> Error;

        void Disconnect();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chap4
{
    public interface IChatConnection
    {
        event Action<string> Received;
        event Action Closed;
        event Action<Exception> Error;
        event Action Connected;

        void Disconnect();
    }
}

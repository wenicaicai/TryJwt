using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Server.Services
{
    public interface IAsParallelLinQService
    {
        string Print();

        string ThreadUnsafe();

        string ThreadSafe();

        string ThreadDefault();
    }
}

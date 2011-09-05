using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.SimpleData
{
    public interface IDatabaseFactory : IDisposable
    {
        Database Get();
    }
}

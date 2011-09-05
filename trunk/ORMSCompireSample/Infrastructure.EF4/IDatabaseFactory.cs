using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.EF4
{
    public interface IDatabaseFactory:IDisposable
    {
        Database Get();
    }
}

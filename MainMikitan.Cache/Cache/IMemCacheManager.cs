using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Cache.Cache
{
    public interface IMemCacheManager
    {
        bool Set(string key, object value);
        T? Get(string key);
        bool Remove(string key);
        void Clear();
    }
}

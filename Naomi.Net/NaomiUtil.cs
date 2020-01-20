using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Naomi.Net
{
    internal static class NaomiUtil
    {
        internal static int GenerateMask(this int[] indexes) => indexes.Aggregate((c, n) => (int)Math.Pow(c, 2) | (int)Math.Pow(n, 2));
    }
}
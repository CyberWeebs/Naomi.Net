using System;

namespace Naomi.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var naomi = new Net.Naomi("API_KEY_HERE");
            naomi.GetSearchResults("https://i.pximg.net/img-master/img/2019/12/31/23/17/18/78616516_p0_master1200.jpg").GetAwaiter().GetResult();
        }
    }
}
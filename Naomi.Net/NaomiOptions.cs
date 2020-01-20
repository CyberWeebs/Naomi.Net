using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Naomi.Net
{
    public class NaomiOptions
    {
        internal static readonly NaomiOptions Default = new NaomiOptions();

        public int Results = 5;
        public int[] Include;
        public int[] Exclude;
        public bool TestMode = false;
        public int? Index;

        private Dictionary<string, object> BuildBasicContent(Dictionary<string, object> content)
        {
            if (Include != null && Exclude != null)
            {
                throw new System.Exception("Include and Exclude options are mutually exclusive.");
            }

            content["output_type"] = 2; // Our output must be JSON.
            content["testmode"] = (TestMode ? 1 : 0);
            content["numres"] = Results;

            // Database Index WILL be used over Include and Exclude (if set).
            if (Index != null)
            {
                content["db"] = Index;
            }
            else if (Include != null)
            {
                content["dbmask"] = Include.GenerateMask();
            }
            else if (Exclude != null)
            {
                content["dbmaski"] = Exclude.GenerateMask();
            }
            else if (Index == null)
            {
                // Nothing is set, fall back to 999 (all indexes).
                content["db"] = 999;
            }

            return content;
        }

        internal Dictionary<string, object> BuildContent(string file, string apiKey)
        {
            var content = new Dictionary<string, object>();
            content = BuildBasicContent(content);

            content["url"] = file;
            content["api_key"] = apiKey;

            return content;
        }
    }
}
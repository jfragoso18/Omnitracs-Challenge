using System;
using System.Collections.Generic;
using System.Text;

namespace Omnitracs_APICSV
{
    /**
     *
     * @author : Jesus Fragoso 
     * @description: Class for Json Serialization, used by the request for the Weather API
     * The Weather API returns different type of data. We need Weather key, and Main Key
     * 
     */

    public class WeatherJson
    {
        public IList<Dictionary<string,string>> Weather { get; set;  }
        public IDictionary<string, string> Main { get; set;  }


    }
}

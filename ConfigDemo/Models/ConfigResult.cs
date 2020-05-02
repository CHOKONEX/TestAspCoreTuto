using System.Collections.Generic;

namespace ConfigDemo.Models
{
    public class ConfigResult
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}

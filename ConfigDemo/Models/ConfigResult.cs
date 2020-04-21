using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigDemo.Models
{
    public class ConfigResult
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}

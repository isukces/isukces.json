using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace isukces.json.test
{
    class TestStruct
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("length")]
        [DefaultValue(0.0)]
        public double Length { get; set; }
    }
}

using System.ComponentModel;
using Newtonsoft.Json;

namespace iSukces.Json.test;

internal class TestStruct
{
    [JsonProperty("color")]
    public string Color { get; set; }

    [JsonProperty("length")]
    [DefaultValue(0.0)]
    public double Length { get; set; }
}
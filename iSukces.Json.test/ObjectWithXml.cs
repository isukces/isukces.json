using System.Xml.Linq;
using Newtonsoft.Json;

namespace iSukces.Json.test;

internal class ObjectWithXml
{
    [JsonConverter(typeof(XDocumentStringConverter))]
    public XDocument Xml { get; set; }
}
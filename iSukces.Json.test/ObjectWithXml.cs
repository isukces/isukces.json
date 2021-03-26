using System.Xml.Linq;
using Newtonsoft.Json;

namespace isukces.json.test
{
    class ObjectWithXml
    {
        [JsonConverter(typeof(XDocumentStringConverter))]
        public XDocument Xml { get; set; }
    }
}
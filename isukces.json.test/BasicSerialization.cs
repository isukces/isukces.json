using System.Xml.Linq;
using Xunit;

namespace isukces.json.test
{
    public class BasicSerialization
    {
        [Fact]
        [Xunit.Trait("Category", "Basic")]
        public static void T01_emptyStruct()
        {
            var a = new TestStruct();
            var b = JsonUtils.Default.Serialize(a, Newtonsoft.Json.Formatting.None);
            // {"color":null,"length":0.0}
            Assert.Equal("{}", b);
        }

        [Fact]
        [Xunit.Trait("Category", "Basic")]
        public static void T02_document_with_xml()
        {
            const string samplexml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><root><a></a></root>";

            const string expectedSerialized = @"{""Xml"":""<?xml version=\""1.0\"" encoding=\""utf-16\"" standalone=\""yes\""?>\r\n<root>\r\n  <a></a>\r\n</root>""}";
            var g = new ObjectWithXml
            {
                Xml = XDocument.Parse(samplexml)
            };
            var v1 = g.Xml.ToString();
            var serialized1 = JsonUtils.Default.Serialize(g, Newtonsoft.Json.Formatting.None);
            g = JsonUtils.Default.Deserialize < ObjectWithXml>(serialized1);
            var v2 = g.Xml.ToString();
            var serialized2 = JsonUtils.Default.Serialize(g, Newtonsoft.Json.Formatting.None);
            Assert.Equal(serialized1, serialized2);
            Assert.Equal(v1, v2);
            Assert.Equal(expectedSerialized, serialized1);
        }
    }
}
using Xunit;

namespace isukces.json.test
{
    public class BasicSerialization
    {
        [Fact]
        public static void T01_emptyStruct()
        {
            var a = new TestStruct();
            var b = JsonUtils.Default.Serialize(a, Newtonsoft.Json.Formatting.None);
            // {"color":null,"length":0.0}
            Assert.Equal("{}", b);
        }
    }
}
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace isukces.json
{
    /// <summary>
    /// 
    /// </summary>
    public class XDocumentStringConverter : JsonConverter
    {
        #region Methods

        // Public Methods 

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(XDocument);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value;
            if (value == null)
                return null;
            var text = value as string;
            if (text == null)
                throw new NotSupportedException("Value must be a string");
            return string.IsNullOrEmpty(text) ? null : XDocument.Parse(text);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var xDocument = value as XDocument;
            if (xDocument == null)
                throw new NotImplementedException();

            var builder = new StringBuilder();
            using (TextWriter textWriter = new StringWriter(builder))
            {
                xDocument.Save(textWriter);
            }
            var serialized = builder.ToString();

            writer.WriteValue(serialized);
        }

        #endregion Methods
    }
}

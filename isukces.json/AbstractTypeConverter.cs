using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace iSukces.Json;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TConcrete"></typeparam>
/// <typeparam name="TAbstract"></typeparam>
public sealed class AbstractTypeConverter<TAbstract, TConcrete> : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TAbstract);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return serializer.Deserialize<TConcrete>(reader);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // nothing to do - just serialize
        serializer.Serialize(writer, value);
    }
}
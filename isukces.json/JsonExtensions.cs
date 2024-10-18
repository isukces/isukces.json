using System;
using Newtonsoft.Json;

namespace iSukces.Json;

/// <summary>
///  Newtonsoft.Json objects extensions
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Appends AbstractTypeConverter that allows to deserialize abstract types or interfaces
    /// </summary>
    /// <param name="src"></param>
    /// <typeparam name="TAbstract"></typeparam>
    /// <typeparam name="TConcrete"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static JsonSerializer WithAbstractTypeConverter<TAbstract, TConcrete>(this JsonSerializer src) where TConcrete : TAbstract
    {
        if (src == null) throw new ArgumentNullException(nameof(src));
        src.Converters.Add(new AbstractTypeConverter<TAbstract, TConcrete>());
        return src;
    }


    public static Func<JsonSerializer> WithProcessing(this Func<JsonSerializer> src, Action<JsonSerializer> action)
    {
        return MyAction;

        JsonSerializer MyAction()
        {
            var jsonSerializer = src();
            action(jsonSerializer);
            return jsonSerializer;
        }
    }
}
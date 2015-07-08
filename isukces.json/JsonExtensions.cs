using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace isukces.json
{
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
        public static JsonSerializer WithAbstractTypeConverter<TAbstract, TConcrete>(this JsonSerializer src)
        {
            if (src == null) throw new ArgumentNullException("src");
            src.Converters.Add(new AbstractTypeConverter<TAbstract, TConcrete>());
            return src;
        }


        public static Func<JsonSerializer> WithProcessing(this Func<JsonSerializer> src, Action<JsonSerializer> action)
        {
            Func<JsonSerializer> func = () =>
            {
                var jsonSerializer = src();
                action(jsonSerializer);
                return jsonSerializer;
            };
            return func;
        }
    }
}

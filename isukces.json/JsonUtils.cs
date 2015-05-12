using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
// ReSharper disable UnusedMember.Global

namespace isukces.json
{
    public class JsonUtils
    {
        #region Constructors

        // ReSharper disable once MemberCanBePrivate.Global
        public JsonUtils(Func<JsonSerializer> serializerFactory)
        {
            SerializerFactory = serializerFactory;
        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 

        // ReSharper disable once MemberCanBePrivate.Global
        public static JsonSerializer DefaultSerializerFactory()
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new StringEnumConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
            return serializer;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public T Deserialize<T>(string json)
        {
            var serializer = SerializerFactory();
            using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
                return serializer.Deserialize<T>(jsonReader);
        }

        public T Load<T>(FileInfo file)
        {

            if (file == null)
                throw new ArgumentNullException("file");
            if (!file.Exists)
                return default(T);

            using (var fileStream = File.Open(file.FullName, FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            using (var textReader = new JsonTextReader(reader))
            {
                var serializer = SerializerFactory();
                return serializer.Deserialize<T>(textReader);
            }
        }

        public List<T> LoadList<T>(FileInfo file)
        {

            if (file == null)
                throw new ArgumentNullException("file");
            if (!file.Exists)
                return null;

            using (var fileStream = File.Open(file.FullName, FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            using (var textReader = new JsonTextReader(reader))
            {
                var jsonSerializer = new JsonSerializer();
                return jsonSerializer.Deserialize<IEnumerable<T>>(textReader).ToList();
            }
        }

        public void Save<T>(FileInfo file, T data, Formatting f = Formatting.Indented)
        {
            if (file == null)
                throw new ArgumentNullException("file");
            using (var fileStream = File.Open(file.FullName, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.Formatting = f; // Formatting.Indented;
                var serializer = SerializerFactory();
                serializer.Serialize(jsonTextWriter, data);
            }
        }

        public string Serialize<T>(T o, Formatting formatting)
        {
            if (o == null)
                return null;
            using (var stringWriter = new StringWriter())
            {
                using (var jsonWriter = new JsonTextWriter(stringWriter))
                {
                    jsonWriter.Formatting = formatting; // Formatting.None;
                    var serializer = SerializerFactory();
                    serializer.Serialize(jsonWriter, o);
                }

                var serialized = stringWriter.ToString();
                return serialized;
            }
        }

        #endregion Methods

        #region Static Properties

        public static JsonUtils Default
        {
            get
            {
                return InstanceHolder.Instance;
            }
        }

        #endregion Static Properties

        #region Properties

        // ReSharper disable once MemberCanBePrivate.Global
        public Func<JsonSerializer> SerializerFactory { get; set; }

        #endregion Properties

        #region Nested Classes


        static class InstanceHolder
        {
            #region Static Fields

            public static readonly JsonUtils Instance = new JsonUtils(DefaultSerializerFactory);

            #endregion Static Fields
        }
        #endregion Nested Classes
    }


}
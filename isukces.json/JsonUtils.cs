#region using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#endregion

// ReSharper disable UnusedMember.Global

namespace isukces.json
{
    public class JsonUtils
    {
        #region Constructors

        // ReSharper disable once MemberCanBePrivate.Global
        public JsonUtils(Func<JsonSerializer> serializerFactory)
        {
            SerializerFactory = serializerFactory;
        }

        #endregion

        #region Static Methods

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

        #endregion

        #region Static Properties

        public static JsonUtils Default => InstanceHolder.Instance;

        #endregion

        #region Properties

        // ReSharper disable once MemberCanBePrivate.Global
        public Func<JsonSerializer> SerializerFactory { get; set; }

        #endregion

        #region Nested

        private static class InstanceHolder
        {
            #region Static Fields

            public static readonly JsonUtils Instance = new JsonUtils(DefaultSerializerFactory);

            #endregion
        }

        #endregion

        #region Methods

        // Public Methods 

        public T Deserialize<T>(string json)
        {
            var serializer = SerializerFactory();
            using(JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
                return serializer.Deserialize<T>(jsonReader);
        }

        public object Deserialize(string json, Type objectType)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            var serializer = SerializerFactory();
            using(JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
                return serializer.Deserialize(jsonReader, objectType);
        }


        public T Load<T>(FileInfo file, IJsonSemaphore mutex)
        {
            if (mutex == null)
                throw new ArgumentNullException(nameof(mutex));
            return Sync.Calc(mutex, () => Load<T>(file));
        }

        public T Load<T>(FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                return default(T);

            using(var fileStream = File.Open(file.FullName, FileMode.Open))
            using(var reader = new StreamReader(fileStream))
            using(var textReader = new JsonTextReader(reader))
            {
                var serializer = SerializerFactory();
                return serializer.Deserialize<T>(textReader);
            }
        }


        public List<T> LoadList<T>(FileInfo file, IJsonSemaphore mutex)
        {
            return Sync.Calc(mutex, () => LoadList<T>(file));
        }

        public List<T> LoadList<T>(FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (!file.Exists)
                return null;

            using(var fileStream = File.Open(file.FullName, FileMode.Open))
            using(var reader = new StreamReader(fileStream))
            using(var textReader = new JsonTextReader(reader))
            {
                var jsonSerializer = new JsonSerializer();
                return jsonSerializer.Deserialize<IEnumerable<T>>(textReader).ToList();
            }
        }

        public void Save<T>(FileInfo file, T data, IJsonSemaphore mutex, Formatting f = Formatting.Indented)
        {
            Sync.Exec(mutex, () => Save(file, data, f));
        }

        public void Save<T>(FileInfo file, T data, Formatting f = Formatting.Indented)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            using(var fileStream = File.Open(file.FullName, FileMode.Create))
            using(var streamWriter = new StreamWriter(fileStream))
            using(var jsonTextWriter = new JsonTextWriter(streamWriter))
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
            using(var stringWriter = new StringWriter())
            {
                using(var jsonWriter = new JsonTextWriter(stringWriter))
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
    }
}
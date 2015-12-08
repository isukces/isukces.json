isukces.json
======

isukces.json consists of

 - JsonUtils - helper class for serializing/deserializing objects
 - Extension class
 - Converters

# JsonUtils #

Allows to serialize and deserialize objects i.e.

```cs
var serialized = JsonUtils.Default.Serialize(myObject, Newtonsoft.Json.Formatting.Indented);
var deserialied = utils.Deserialize<MyType>(serialized);
```

# Converters #


## AbstractTypeConverter ##

Allows to deserialize abstract types or interfaces by providing concrete type.

```cs
var jsonUtils = JsonUtils.Default;

jsonUtils.SerializerFactory = jsonUtils.SerializerFactory
    .WithProcessing(serializer =>
            {
                serializer.WithAbstractTypeConverter<ISomeInterface, MyImplementation>();
            });
            
var deserialied = utils.Deserialize<ISomeInterface>(serialized);            
```

## XDocumentStringConverter ##

Allows to serialize XDocument class to string

```cs
class ObjectWithXml
{
    [JsonConverter(typeof(XDocumentStringConverter))]
    public XDocument Xml { get; set; }
}
```


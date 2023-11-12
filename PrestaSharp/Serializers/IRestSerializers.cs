using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers;

namespace Bukimedia.PrestaSharp.Serializers
{
    class XmlRestSerializer : IRestSerializer
    {
        public ISerializer Serializer { get; } = new PrestaSharpSerializer();
        public IDeserializer Deserializer { get; } = new PrestaSharpDeserializer();

        public string[] AcceptedContentTypes { get; } = new[]
        {
            "text/xml"
        };

        public SupportsContentType SupportsContentType { get; } = contentType => contentType.Value.Contains("/xml");

        public DataFormat DataFormat { get; } = DataFormat.Xml;

        public string Serialize(Parameter parameter)
        {
            return Serializer.Serialize(parameter.Value);
        }
    }

    class TextErrorSerializer : IRestSerializer
    {
        public ISerializer Serializer { get; } = new PrestaSharpSerializer();
        public IDeserializer Deserializer { get; } = new PrestaSharpTextErrorDeserializer();

        public string[] AcceptedContentTypes { get; } = new[]
        {
            "text/html"
        };

        public SupportsContentType SupportsContentType { get; } = contentType => contentType.Value.Contains("/html");

        public DataFormat DataFormat { get; } = DataFormat.None;

        public string Serialize(Parameter parameter)
        {
            throw new NotImplementedException();
        }
    }
}

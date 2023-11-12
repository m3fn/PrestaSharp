using RestSharp;
using System;

using RestSharp.Serializers.Xml;

namespace Bukimedia.PrestaSharp.Serializers
{
    public class PrestaSharpTextErrorDeserializer : IXmlDeserializer
    {
        public T Deserialize<T>(RestResponse response)
        {
            throw new Exception("Prestashop failed to serve XML response instead got text: " + response.Content);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
    }
}
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;
using System;

namespace Adventure
{
    public interface IReferable
    {
        string Name { get; set; }
    }

    // convert references of an object in JSON to their actual object when loaded
    public class ReferenceConverter<T> : JsonConverter<T> where T: IReferable
    {
        private Dictionary<string, T> references;

        public ReferenceConverter(Dictionary<string, T> references)
        {
            this.references = references;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException();

            string reference = reader.GetString();
            if (!references.ContainsKey(reference))
                throw new JsonException($"Item {reference} does not exist and failed to load");

            return references[reference];
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace VigiothCapital.QuantTrader.Util
{
    /// <summary>
    /// Reads json and always produces a List, even if the input has just an object
    /// </summary>
    public class SingleValueListConverter<T> : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object. If the instance is not a list then it will
        /// be wrapped in a list
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is T)
            {
                value = new List<T> {(T)value};
            }
            serializer.Serialize(writer, value);
        }
        /// <summary>
        /// Reads the JSON representation of the object. If the JSON represents a singular instance, it will be returned
        /// in a list.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    return new List<T> {serializer.Deserialize<T>(reader)};
                case JsonToken.StartArray:
                    return serializer.Deserialize<List<T>>(reader);
                default:
                    throw new ArgumentException("The JsonReader is expected to point at a JsonToken.StartObject or JsonToken.StartArray.");
            }
        }
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T) || objectType == typeof(List<T>);
        }
    }
}
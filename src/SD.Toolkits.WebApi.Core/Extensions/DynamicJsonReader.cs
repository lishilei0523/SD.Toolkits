using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SD.Toolkits.WebApi.Core.Extensions
{
    /// <summary>
    ///     Temp Dynamic Converter
    ///     by:tchivs@live.cn
    /// </summary>
    public class DynamicJsonReader : JsonConverter<dynamic>
    {
        public override dynamic Read(ref Utf8JsonReader jsonReader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (jsonReader.TokenType == JsonTokenType.True)
            {
                return true;
            }
            if (jsonReader.TokenType == JsonTokenType.False)
            {
                return false;
            }
            if (jsonReader.TokenType == JsonTokenType.Number)
            {
                if (jsonReader.TryGetInt32(out int int32))
                {
                    return int32;
                }
                if (jsonReader.TryGetInt64(out long int64))
                {
                    return int64;
                }

                return jsonReader.GetDouble();
            }
            if (jsonReader.TokenType == JsonTokenType.String)
            {
                if (jsonReader.TryGetDateTime(out DateTime datetime))
                {
                    return datetime;
                }

                return jsonReader.GetString();
            }
            if (jsonReader.TokenType == JsonTokenType.StartObject)
            {
                using JsonDocument documentV = JsonDocument.ParseValue(ref jsonReader);

                return ReadObject(documentV.RootElement);
            }

            // Use JsonElement as fallback.
            // Newtonsoft uses JArray or JObject.
            JsonDocument document = JsonDocument.ParseValue(ref jsonReader);

            return document.RootElement.Clone();
        }

        private static object ReadObject(JsonElement jsonElement)
        {
            IDictionary<string, object> expandoObject = new ExpandoObject();
            foreach (JsonProperty jsonProperty in jsonElement.EnumerateObject())
            {
                string key = jsonProperty.Name;
                object value = ReadValue(jsonProperty.Value);
                expandoObject[key] = value;
            }

            return expandoObject;
        }

        private static object ReadValue(JsonElement jsonElement)
        {
            object result = null;
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Object:
                    result = ReadObject(jsonElement);
                    break;
                case JsonValueKind.Array:
                    result = ReadList(jsonElement);
                    break;
                case JsonValueKind.String:
                    //TODO: Missing Bytes Convert
                    if (jsonElement.TryGetDateTime(out DateTime datetime))
                    {
                        result = datetime;
                    }
                    else
                    {
                        result = jsonElement.GetString();
                    }
                    break;
                case JsonValueKind.Number:

                    if (jsonElement.TryGetInt32(out int int32))
                    {
                        result = int32;
                    }
                    else if (jsonElement.TryGetInt64(out long int64))
                    {
                        result = int64;
                    }
                    else
                    {
                        result = jsonElement.GetDouble();
                    }
                    break;
                case JsonValueKind.True:
                    result = true;
                    break;
                case JsonValueKind.False:
                    result = false;
                    break;
                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public static IList<object> ReadList(JsonElement jsonElement)
        {
            IList<object> list = new List<object>();
            foreach (JsonElement item in jsonElement.EnumerateArray())
            {
                list.Add(ReadValue(item));
            }

            return list.Count == 0 ? null : list;
        }

        public static object[] ReadArray(JsonElement jsonElement)
        {
            object[] array = new object[jsonElement.GetArrayLength()];
            int index = 0;
            foreach (JsonElement jsonElementItem in jsonElement.EnumerateArray())
            {
                array[index] = ReadValue(jsonElementItem);
                index++;
            }

            return array.Length == 0 ? null : array;
        }

        public override void Write(Utf8JsonWriter jsonWriter, object value, JsonSerializerOptions options)
        {

        }
    }
}

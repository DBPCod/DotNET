using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Configs;

public class JsonNullEmptyIgnoreConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(string);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return new NullEmptyStringConverter();
    }

    private class NullEmptyStringConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                // Bỏ qua hoàn toàn thuộc tính với giá trị rỗng hoặc null
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value);
            }
        }
    }
}

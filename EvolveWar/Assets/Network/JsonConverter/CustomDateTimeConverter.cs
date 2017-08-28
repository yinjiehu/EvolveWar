using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Network.Data
{
    public class CustomDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type object_type)
        {
            if (object_type == typeof(DateTime))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type object_type, object existing_value, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Date)
            {
                throw new JsonSerializationException(
					string.Format("Unexpected token parsing date. Expected String or Date, got {0}.", object_type.Name));
            }

            DateTime ret;
            if (!DateTime.TryParse(reader.Value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out ret))
            {
                var ticks = long.Parse(reader.Value.ToString().Split(';')[0]);
                ret = new DateTime(ticks, DateTimeKind.Utc);
            }
            return ret;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dateTime = (DateTime)value;
            writer.WriteValue(dateTime.Ticks + ";" + dateTime.ToString());
        }
    }
}
using Newtonsoft.Json;
using System;
using System.IO;

namespace Network.Data
{
	public abstract class AbstractCustomBinaryConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type object_type)
        {
            return object_type == typeof(T);
        }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var obj = (T)value;

			MemoryStream ms = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(ms);

			this.CustomWrite(bw, obj);

			bw.Close();
			ms.Close();

			var bytes = ms.ToArray();
			writer.WriteValue(Convert.ToBase64String(bytes));
		}

		protected abstract void CustomWrite(BinaryWriter bw, T obj);

		public override object ReadJson(JsonReader reader, Type object_type, object existing_value, JsonSerializer serializer)
        {
			var bytes = Convert.FromBase64String((string)reader.Value);
			MemoryStream ms = new MemoryStream(bytes);
            BinaryReader br = new BinaryReader(ms);

			var ret = this.CustomRead(br);

			br.Close();
			ms.Close();

            return ret;
        }

		protected abstract T CustomRead(BinaryReader br);
	}
}
using Haruna.OpenXml;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace BattleWorld.Data
{
	[Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class DatedSettings
    {
        [SerializeField]
        [JsonProperty]
        protected DateStamp _createDate = new DateStamp(DateTime.UtcNow);
        [XmlParseIgnore]
        public DateStamp CreateDate { get { return _createDate; } }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public struct DateStamp
    {
        [SerializeField]
        [JsonProperty]
        long _tick;
        [XmlParseIgnore]//在Req和Res里调用自定义类型的时候，类型自身变量必须有property，property必须有get和set;
        public long Tick { get { return _tick; } }

        [SerializeField]
        [JsonProperty]
        string _formattedString;
        [XmlParseIgnore]
        public string FormattedString { get { return _formattedString; } }

        public DateStamp(DateTime dt)
        {
            this._tick = dt.Ticks;
            this._formattedString = FormatDateString(dt);
        }

        public override string ToString()
        {
            return this._formattedString ?? FormatDateString(DateTime.MinValue);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private static string FormatDateString(DateTime dt)
        { 
            return dt.ToString("yyyy-M-d HH:mm:ss.fff");
        }

        public static bool operator ==(DateStamp a, DateStamp b)
        {
            return a._tick == b._tick;
        }

        public static bool operator !=(DateStamp a, DateStamp b)
        {
            return a._tick != b._tick;
        }

        public static bool operator <(DateStamp a, DateStamp b)
        {
            return a._tick < b._tick;
        }

        public static bool operator >(DateStamp a, DateStamp b)
        {
            return a._tick > b._tick;
        }

        public static bool operator <=(DateStamp a, DateStamp b)
        {
            return a._tick <= b._tick;
        }

        public static bool operator >=(DateStamp a, DateStamp b)
        {
            return a._tick >= b._tick;
        }
    }
}
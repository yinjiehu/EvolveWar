using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kit.Json
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class JsonIgnoreWhenDB : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class JsonIgnoreWhenInternet : Attribute
	{
	}

	public class JsonConditionIgnoreContractResolver<T> : DefaultContractResolver where T : Attribute
	{
		//HashSet<string> _ignoreStrs = new HashSet<string>();

		//public JsonConditionIgnoreContractResolver(params string[] toIgnoreStrs)
		//{
		//	_ignoreStrs.UnionWith(toIgnoreStrs);
		//}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
#if BATTLEWORLD_SERVER
			var property = base.CreateProperty(member, memberSerialization);
			var attr = member.GetCustomAttribute<T>(true);
			if (attr != null)
				property.ShouldSerialize = ins => false;

			return property;
#else
			return base.CreateProperty(member, memberSerialization);
#endif
		}
	}
}

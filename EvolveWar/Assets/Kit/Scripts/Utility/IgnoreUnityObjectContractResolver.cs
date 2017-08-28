using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kit.Utility
{
	public class IgnoreUnityObjectContractResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty property = base.CreateProperty(member, memberSerialization);

			if (property.PropertyType.IsSubclassOf(typeof(UnityEngine.Object)) &&
				member.GetCustomAttributes(typeof(JsonPropertyAttribute), true).Length == 0)
			{
				property.ShouldSerialize = (instance) => { return false; };
			}
			else if (property.PropertyType.IsGenericType)
			{
				var genericArgs = property.PropertyType.GetGenericArguments();
				foreach (var g in genericArgs)
				{
					if (g.IsSubclassOf(typeof(UnityEngine.Object)) && member.GetCustomAttributes(typeof(JsonPropertyAttribute), true).Length == 0)
					{
						property.ShouldSerialize = (instance) => { return false; };
						break;
					}
				}
			}
			return property;
		}
	}
}

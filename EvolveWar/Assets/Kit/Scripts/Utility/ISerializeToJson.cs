
using Newtonsoft.Json;
using System;
using System.IO;

namespace Kit.Utility
{
#if UNITY_EDITOR
	public interface ISerializeToJson
	{
		void SerializeToJson(bool dialogConfirm = true);
	}
#else
	public interface ISerializeToJson
	{
	}
#endif
}
using System;
using System.Runtime.Serialization;

namespace Network.Data
{
	public class JsonInterfaceBinder : SerializationBinder
	{
#if IRONFURY_SERVER
		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			var thisAssembly = this.GetType().Assembly;
			if (thisAssembly == serializedType.Assembly)
				assemblyName = "";
			else
				assemblyName = serializedType.Assembly.FullName;
			
			typeName = serializedType.FullName;
			//typeName = typeName.Replace(", DataAccess", "");
		}
		
#endif
		public override Type BindToType(string assemblyName, string typeName)
		{
			var t = Type.GetType(typeName);
			if (t != null)
			{
				return t;
			}

			//var assembly = System.Reflection.Assembly.Load("IronFuryHot");
			//t = assembly.GetType(typeName);
			//if (t != null)
			//	return t;

			
			var assembly = System.Reflection.Assembly.Load(assemblyName);
			return assembly.GetType(typeName);
		}

	}
}

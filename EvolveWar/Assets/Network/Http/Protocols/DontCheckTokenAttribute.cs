using System;

namespace Network.Http.Protocols
{	
	[AttributeUsage(AttributeTargets.Field)]
	public class DontCheckTokenAttribute : Attribute
	{
	}
}

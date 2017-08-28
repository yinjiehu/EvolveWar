using System;

namespace Network.Http.Protocols
{
	public enum ConnectTargetEnum
	{
		Game,
		Account,
		Download,
		Route,
	}
	
	[AttributeUsage(AttributeTargets.Class)]
	public class ConnectServerAttribute : Attribute
	{
		public ConnectTargetEnum ConnectTarget { private set; get; }
		public ConnectServerAttribute(ConnectTargetEnum server)
		{
			ConnectTarget = server;
		}
	}
}

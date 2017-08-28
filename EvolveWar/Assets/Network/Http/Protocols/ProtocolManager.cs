using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using Network.Utillity;

namespace Network.Http.Protocols
{
	public class ProtocolManager : MonoBehaviour
	{
		static List<Protocol> _procotols;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Init()
		{
			_procotols = new List<Protocol>();

			var types = from assembly in AppDomain.CurrentDomain.GetAssemblies()
						from type in assembly.GetTypes()
						where type.Name.Contains("Protocol")
						select type;
			
			foreach (var t in types)
			{
				ConnectTargetEnum connectTarget = ConnectTargetEnum.Game;
				var connectServerAttr = t.GetCustomAttributes(typeof(ConnectServerAttribute), true) as ConnectServerAttribute[];
				if (connectServerAttr.Length != 0)
					connectTarget = connectServerAttr[0].ConnectTarget;

				var fields = t.GetFields().Where(f => f.FieldType.IsSubclassOf(typeof(Protocol)));
				foreach (var f in fields)
				{
                    var dontCheckTokenAttr = f.GetCustomAttributes(typeof(DontCheckTokenAttribute), true);
					
					var protocol = f.GetValue(null) as Protocol;
					if (protocol == null)
					{
						protocol = Activator.CreateInstance(f.FieldType) as Protocol;
						f.SetValue(null, protocol);
					}
					protocol.Init(
						connectTarget, 
						t.Name.Replace("Protocol", "/" + f.Name),
						dontCheckTokenAttr.Length == 0);

					_procotols.Add(protocol);
				}
			}
			Debug.Log("protocols has initialized");
		}

	}
}
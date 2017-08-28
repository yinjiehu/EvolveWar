using Network.Code;
using System;

namespace IronFury.Infrastructure.Exceptions
{
	public class ActionLogicException : Exception
	{
		public CodeMessage Code { set; get; }

		public string ExtraMessage { set; get; }

		public ActionLogicException(CodeMessage code)
		{
			Code = code;
		}
		public ActionLogicException(CodeMessage code, string extraMsg)
		{
			Code = code;
			ExtraMessage = extraMsg;
		}

		public override string Message
		{
			get
			{
				if(string.IsNullOrWhiteSpace(ExtraMessage))
					return Code.ToString();

				return Code.ToString() + "\n" + ExtraMessage;
			}
		}
	}
}
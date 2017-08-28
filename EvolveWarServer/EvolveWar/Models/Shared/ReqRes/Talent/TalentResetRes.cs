using Model;
using Network.Code;

namespace Shared.Talent
{
	public class TalentResetRes
	{
		public CodeMessage Result { set; get; }

		public PlayerStatus Player { set; get; }

		public TalentResetRes()
		{
			Result = ReturnCode.OK;
		}

		public TalentResetRes(CodeMessage code)
		{
			Result = code;
		}
	}
}

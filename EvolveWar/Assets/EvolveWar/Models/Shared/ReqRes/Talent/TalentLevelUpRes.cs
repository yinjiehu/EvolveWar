using Model;
using Network.Code;

namespace Shared.Talent
{
	public class TalentLevelUpRes
	{
		public CodeMessage Result { set; get; }

		public PlayerStatus Player { set; get; }

		public TalentLevelUpRes()
		{
			Result = ReturnCode.OK;
		}

		public TalentLevelUpRes(CodeMessage code)
		{
			Result = code;
		}
	}
}

﻿using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Network.Code;

namespace Shared.Rank
{
	public class RankingInfo
	{
		public string DisplayName { set; get; }
		public int Level { set; get; }
		public long Kill { set; get; }
		public long Dead { set; get; }
	}

	public class GetRankingListRes
	{
		public CodeMessage Result { set; get; }

		public List<RankingInfo> RankingList;

		public GetRankingListRes()
		{
			Result = ReturnCode.OK;
		}

		public GetRankingListRes(CodeMessage code)
		{
			Result = code;
		}
	}
	
}
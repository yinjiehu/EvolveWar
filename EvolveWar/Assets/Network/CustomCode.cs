
namespace Network.Code
{
	public struct CodeMessage
	{
		public string Code;
		public string Message;
		public CodeMessage(string c, string m)
		{
			Code = c;
			Message = m;
		}
		public override string ToString()
		{
			return Code + ";" + Message;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if(obj is CodeMessage)
				return this.Code == ((CodeMessage)obj).Code;
			return false;
		}
		public static bool operator ==(CodeMessage c1, CodeMessage c2)
		{
			return c1.Code == c2.Code;
		}
		public static bool operator !=(CodeMessage c1, CodeMessage c2)
		{
			return c1.Code != c2.Code;
		}
	}

	public class StatusCode
	{
		public const string KEY_PID = "IRONFURY-PROCESS";
		public const string KEY_STS = "IRONFURY-STATUS";

		public const string CODE_OK = "OK";		
	}

	public class ReturnCode
	{
		/// <summary>
		/// 正常
		/// </summary>
		public static CodeMessage OK = new CodeMessage("OK", "");

		/// <summary>
		/// 用户名已存在
		/// </summary>
		public static CodeMessage A0010L = new CodeMessage("A0010L", "用户名已存在");
        /// <summary>
        /// 用户名已存在
        /// </summary>
        public static CodeMessage A0011L = new CodeMessage("A0011L", "平台用户名已存在");
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        public static CodeMessage A0030L = new CodeMessage("A0030L", "用户名或密码错误");
		/// <summary>
		/// 渠道用户登录验证失败
		/// </summary>
		public static CodeMessage A0031L = new CodeMessage("A0031L", "渠道用户登录验证失败");
		/// <summary>
		/// 昵称已存在
		/// </summary>
		public static CodeMessage A0040L = new CodeMessage("A0040L", "昵称已存在");
        /// <summary>
		/// 玩家不存在
		/// </summary>
		public static CodeMessage A0041L = new CodeMessage("A0041L", "玩家不存在");
        /// <summary>
        /// 用户名包含非法字符
        /// </summary>
        public static CodeMessage A0042L = new CodeMessage("A0041L", "此名称包含非法字符");
        /// <summary>
        /// 帐号不能超过十六位
        /// </summary>
        public static CodeMessage A0043L = new CodeMessage("A0041L", "帐号不能超过十六位");
		/// <summary>
		/// 密码不低于六位
		/// </summary>
		public static CodeMessage A0044L = new CodeMessage("A0041L", "密码不低于六位");
		/// <summary>
		/// 帐号不低于三位
		/// </summary>
		public static CodeMessage A0045L = new CodeMessage("A0041L", "帐号不低于三位");
		/// <summary>
		/// 服务器ID错误。服务器不存在或不可用
		/// </summary>
		public static CodeMessage A0050L = new CodeMessage("A0050L", "服务器不可用");

		/// <summary>
		/// 需要进行小更新
		/// </summary>
		public static CodeMessage D0001L = new CodeMessage("D0001L", "服务器不可用");
		/// <summary>
		/// 需要进行小更新
		/// </summary>
		public static CodeMessage D0002L = new CodeMessage("D0002L", "服务器不可用");
		
		/// <summary>
		/// 燃料不足无法开始战役关卡
		/// </summary>
		public static CodeMessage G1000L = new CodeMessage("G1000L", "燃料不足无法开始战役关卡");
		public static CodeMessage G1001L = new CodeMessage("G1001L", "关卡不可使用");
		public static CodeMessage G1002L = new CodeMessage("G1002L", "车辆不可用");
		public static CodeMessage G1010L = new CodeMessage("G1010L", "没有查询到战斗开始记录");
        public static CodeMessage G1011L = new CodeMessage("G1011L", "助战信息已过期，请重新选择");


        public static CodeMessage G1020L = new CodeMessage("G1020L", "本日围攻次数已用尽");
		public static CodeMessage G1021L = new CodeMessage("G1021L", "关卡不可使用");
		public static CodeMessage G1022L = new CodeMessage("G1022L", "车辆不可用");
		public static CodeMessage G1030L = new CodeMessage("G1030L", "没有查询到战斗开始记录");

		/// <summary>
		/// 宝箱已获取
		/// </summary>
		public static CodeMessage G1040L = new CodeMessage("G1040L", "宝箱已获取");
		/// <summary>
		/// 未满足获取条件
		/// </summary>
		public static CodeMessage G1041L = new CodeMessage("G1041L", "未满足获取条件");

        public static CodeMessage G1100L = new CodeMessage("G1100L", "扫荡功能未开启");
        public static CodeMessage G1101L = new CodeMessage("G1101L", "未满足获取条件");
        public static CodeMessage G1102L = new CodeMessage("G1102L", "金币与金条不足");
        public static CodeMessage G1103L = new CodeMessage("G1103L", "燃料不足无法扫荡");
        public static CodeMessage G1104L = new CodeMessage("G1104L", "战役关卡不可使用，不可扫荡");
        public static CodeMessage G1105L = new CodeMessage("G1105L", "车辆不可用，无法开始战役关卡");
        
        /// <summary>
        /// 无在线阶段数据
        /// </summary>
        public static CodeMessage G1042L = new CodeMessage("G1042L", "无阶段数据");

		/// <summary>
		/// 已达到每日参战上限
		/// </summary>
		public static CodeMessage G1050L = new CodeMessage("G1050L", "已达到每日参战上限");

		/// <summary>
		/// 车辆未满足解锁条件
		/// </summary>
		public static CodeMessage G2000L = new CodeMessage("G2000L", "车辆未满足解锁条件");
		/// <summary>
		/// 车辆已经解锁
		/// </summary>
		public static CodeMessage G2001L = new CodeMessage("G2001L", "车辆已经解锁");
        /// <summary>
		/// 系统未满足解锁条件
		/// </summary>
		public static CodeMessage G2002L = new CodeMessage("G2001L", "系统未满足解锁条件");


        /// <summary>
        /// 车辆无法使用
        /// </summary>
        public static CodeMessage G2010L = new CodeMessage("G2010L", "车辆无法使用");

		/// <summary>
		/// 无法开始修理
		/// </summary>
		public static CodeMessage G2100L = new CodeMessage("G2100L", "无法开始修理");

		/// <summary>
		/// 无法取消修理，修理已完成
		/// </summary>
		public static CodeMessage G2110L = new CodeMessage("G2110L", "修理已完成");

		/// <summary>
		/// 资源不足，无法进行抢修
		/// </summary>
		public static CodeMessage G2130L = new CodeMessage("G2130L", "资源不足，无法进行抢修");
		/// <summary>
		/// 无法抢修，修理已完成
		/// </summary>
		public static CodeMessage G2131L = new CodeMessage("G2131L", "修理已完成");
		
		/// <summary>
		/// 模块不满足解锁条件
		/// </summary>
		public static CodeMessage G2200L = new CodeMessage("G2200L", "模块不满足解锁条件");
		/// <summary>
		/// 弹药槽不满足解锁条件
		/// </summary>
		public static CodeMessage G2300L = new CodeMessage("G2300L", "弹药槽不满足解锁条件");
		/// <summary>
		/// 无法装填弹药
		/// </summary>
		public static CodeMessage G2310L = new CodeMessage("G2310L", "无法装填弹药");

		/// <summary>
		/// 技能不满足解锁条件
		/// </summary>
		public static CodeMessage G2350L = new CodeMessage("G2350L", "技能不满足解锁条件");
		/// <summary>
		/// 技能不能重复解锁
		/// </summary>
		public static CodeMessage G2360L = new CodeMessage("G2360L", "技能不能重复解锁");

		/// <summary>
		/// 涂装不满足解锁条件
		/// </summary>
		public static CodeMessage G2400L = new CodeMessage("G2400L", "涂装不满足解锁条件");
		/// <summary>
		/// 无法选择涂装
		/// </summary>
		public static CodeMessage G2410L = new CodeMessage("G2410L", "无法选择涂装");
		/// <summary>
		/// 无法使用车组系统
		/// </summary>
		public static CodeMessage G2500L = new CodeMessage("G2500L", "无法使用车组系统");
		/// <summary>
		/// 车组成员不可用
		/// </summary>
		public static CodeMessage G2510L = new CodeMessage("G2510L", "车组成员不可用");

		///// <summary>
		///// 车辆不可用
		///// </summary>
		//public static CodeMessage G2600L = new CodeMessage("G2600L", "车辆不可用");

		/// <summary>
		/// 商城已更新
		/// </summary>
		public static CodeMessage G3000L = new CodeMessage("G3000L", "商城已更新");
		public static CodeMessage G3001L = new CodeMessage("G3001L", "金额不足，购买失败");
        /// <summary>
        /// 商品ID错误，未找到对应商品
        /// </summary>
        public static CodeMessage G3002L = new CodeMessage("G3002L", "商品ID错误，未找到对应商品");

        /// <summary>
        /// 每日签到已经完成
        /// </summary>
        public static CodeMessage G4000L = new CodeMessage("G4000L", "每日签到已经完成");
		/// <summary>
		/// 七日连续签到已经完成
		/// </summary>
		public static CodeMessage G4010L = new CodeMessage("G4010L", "今日签到已经完成");
        /// <summary>
        /// 限时战斗
        /// </summary>
        public static CodeMessage G5000L = new CodeMessage("G5000L", "未满足条件");
        /// <summary>
        /// 限时战斗次数不足
        /// </summary>
        public static CodeMessage G5010L = new CodeMessage("G5010L", "次数不足");
        /// <summary>
        /// 当前不在活动时间范围
        /// </summary>
        public static CodeMessage G5020L = new CodeMessage("G5020L", "当前不在活动时间范围");
		/// <summary>
		/// 车辆不可用
		/// </summary>
		public static CodeMessage G5030L = new CodeMessage("G5030L", "车辆不可用");
		/// <summary>
		/// 过关请求不被承认
		/// </summary>
		public static CodeMessage G5040L = new CodeMessage("G5040L", "未查询到战斗开始记录");
		/// <summary>
		/// 过关请求不被承认
		/// </summary>
		public static CodeMessage G5041L = new CodeMessage("G5041L", "本次战斗已经完成");

		/// <summary>
		/// vip奖励已领取
		/// </summary>
		public static CodeMessage G6000L = new CodeMessage("G6000L", "今日奖励已领取");
        /// <summary>
        /// VIP等级未达到
        /// </summary>
        public static CodeMessage G6010L = new CodeMessage("G6000L", "VIP等级未达到");


		/// <summary>
		/// 未达到活动条件
		/// </summary>
		public static CodeMessage G7000L = new CodeMessage("G7000L", "未达到活动条件");
		

		/// <summary>
		/// PVP未开启
		/// </summary>
		public static CodeMessage G8000L = new CodeMessage("G8000L", "未达到开启PVP战斗的条件");
		/// <summary>
		/// PVP战斗次数已用尽
		/// </summary>
		public static CodeMessage G8010L = new CodeMessage("G8010L", "今日pvp战斗次数已用尽");
        /// <summary>
        /// PVP赛季无奖励信息
        /// </summary>
        public static CodeMessage G8011L = new CodeMessage("G8011L", "PVP赛季无奖励信息");
        /// <summary>
        /// 车辆不可用
        /// </summary>
        public static CodeMessage G8020L = new CodeMessage("G8020L", "车辆不可用");
		/// <summary>
		/// 车辆不可用
		/// </summary>
		public static CodeMessage G8030L = new CodeMessage("G8020L", "未查询到战斗开始记录");


		/// <summary>
		/// 抽奖金额不足
		/// </summary>
		public static CodeMessage G9000L = new CodeMessage("G9000L", "资源不足！");
        /// <summary>
		/// 金条不足，请充值
		/// </summary>
		public static CodeMessage G9001L = new CodeMessage("G9001L", "金条不足，请充值！");
        /// <summary>
		/// 钞票不足，请充值
		/// </summary>
		public static CodeMessage G9002L = new CodeMessage("G9002L", "钞票不足，请充值！");
        /// <summary>
        /// 您被对发拉黑，无法添加好友
        /// </summary>
        public static CodeMessage G10000L = new CodeMessage("G10000L", "您被对方拉黑，无法添加好友");
        /// <summary>
        /// 您被对发拉黑，无法添加好友
        /// </summary>
        public static CodeMessage G10001L = new CodeMessage("G10000L", "您已经添加该好友");
        /// <summary>
        /// 当日发送体力请求次数不足
        /// </summary>
        public static CodeMessage G10010L = new CodeMessage("G10010L", "当日发送体力请求次数不足");
		/// <summary>
		/// 已经向这个人发送过体力请求，无法再次发送
		/// </summary>
		public static CodeMessage G10011L = new CodeMessage("G10011L", "已经发送过体力请求，无法再次发送");
        /// <summary>
        /// 无法同意体力请求，没有查询到对应的记录
        /// </summary>
        public static CodeMessage G10012L = new CodeMessage("G10012L", "无法同意体力请求，没有查询到对应的记录");
        /// <summary>
        /// 已经收过对方同意的体力请求
        /// </summary>
        public static CodeMessage G10013L = new CodeMessage("G10013L", "已经收过对方同意的体力请求");

        /// <summary>
        /// 已经邀请过，无法重新邀请
        /// </summary>
        public static CodeMessage G11000L = new CodeMessage("G11000L", "活动奖励已经领取");
        /// <summary>
        /// 该关卡已经邀请过
        /// </summary>
        public static CodeMessage G12000L = new CodeMessage("G12000L", "该关卡已经邀请过");
        /// <summary>
        /// 无复仇战信息
        /// </summary>
        public static CodeMessage G13000L = new CodeMessage("G13000L", "无复仇战信息");
        /// <summary>
        /// 复仇战信息错误
        /// </summary>
        public static CodeMessage G13001L = new CodeMessage("G13001L", "复仇战信息错误");
		/// <summary>
		/// 可参加次数不足
		/// </summary>
		public static CodeMessage G13002L = new CodeMessage("G13002L", "可参加次数不足");
		/// <summary>
		/// 战斗已过期
		/// </summary>
		public static CodeMessage G13003L = new CodeMessage("G13003L", "战斗已过期");
        /// <summary>
        /// 邮件不存在
        /// </summary>
        public static CodeMessage G14001L = new CodeMessage("G14001L", "邮件不存在");
        /// <summary>
        /// 邮件附件已领取
        /// </summary>
        public static CodeMessage G14002L = new CodeMessage("G14002L", "邮件附件已领取");

		/// <summary>
		/// 无效激活码
		/// </summary>
		public static CodeMessage G15000L = new CodeMessage("G15000L", "该礼包已被其他玩家领取");
		/// <summary>
		/// 输入错误
		/// </summary>
		public static CodeMessage G15001L = new CodeMessage("G15001L", "输入错误");
		/// <summary>
		/// 该礼包码已失效
		/// </summary>
		public static CodeMessage G15002L = new CodeMessage("G15002L", "该礼包码已失效");
		/// <summary>
		/// 数据错误
		/// </summary>
		public static CodeMessage G16000L = new CodeMessage("G16000L", "数据错误");

        /// <summary>
        /// 新手任务未开启
        /// </summary>
        public static CodeMessage G17000L = new CodeMessage("G16000L", "新手任务未开启");
        /// <summary>
        /// 任务不存在
        /// </summary>
        public static CodeMessage G17001L = new CodeMessage("G16000L", "任务不存在");
        /// <summary>
        /// 未达到完成条件
        /// </summary>
        public static CodeMessage G17002L = new CodeMessage("G16000L", "未达到完成条件");
    }

	public class ErrCode
	{
		/// <summary>
		/// 客户端发送数据错误
		/// </summary>
		public static CodeMessage C0000S = new CodeMessage("C0000S", "客户端发送数据错误");
		/// <summary>
		/// 无法获取UID对应的PlayerInfo
		/// </summary>
		public static CodeMessage C0001S = new CodeMessage("C0001S", "无法获取UID对应的PlayerInfo");
		/// <summary>
		/// 无法从Redis中取得相应数据
		/// </summary>
		public static CodeMessage C0002S = new CodeMessage("C0002S", "无法从Redis中取得相应数据");
		/// <summary>
		/// 无法从Config中取得相应数据
		/// </summary>
		public static CodeMessage C0003S = new CodeMessage("C0003S", "无法从Config中取得相应数据");

		/// <summary>
		/// 客户端需要更新
		/// </summary>
		public static CodeMessage D0000S = new CodeMessage("D0000S", "客户端需要更新");
		/// <summary>
		/// 无法获取版本更新文件
		/// </summary>
		public static CodeMessage D0010S = new CodeMessage("D0010S", "无法获取版本更新文件");
		/// <summary>
		/// 无法创建默认用户数据
		/// </summary>
		public static CodeMessage G0000S = new CodeMessage("G0000S", "无法创建默认用户数据");
		/// <summary>
		/// 服务器没有正常启动
		/// </summary>
		public static CodeMessage G0001S = new CodeMessage("G0001S", "服务器没有正常启动");
		/// <summary>
		/// 服务器已停机
		/// </summary>
		public static CodeMessage G0002S = new CodeMessage("G0001S", "服务器已停机");

		/// <summary>
		/// 战役模式过关时服务器数据发生错误
		/// </summary>
		public static CodeMessage G1010S = new CodeMessage("G1010S", "服务器数据发生错误");
		/// <summary>
		/// 围攻模式过关时服务器数据发生错误
		/// </summary>
		public static CodeMessage G1030S = new CodeMessage("G1030S", "服务器数据发生错误");

		/// <summary>
		/// 商品ID错误，商品为充值类商品
		/// </summary>
		public static CodeMessage G3000S = new CodeMessage("G3000S", "商品ID错误");
		/// <summary>
		/// 商品ID错误，商品非充值类商品
		/// </summary>
		public static CodeMessage G3010S = new CodeMessage("G3010S", "商品ID错误");

    }
}

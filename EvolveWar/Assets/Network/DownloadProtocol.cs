using System;

namespace Network.Http.Protocols
{
    public static class DownloadProtocol
    {
		/// <summary>
		/// 检查版本内容
		/// </summary>
		[DontCheckToken]
		public static Protocol<string> GetAndroidVersionXml = new Protocol<string>();
		/// <summary>
		/// 检查版本内容
		/// </summary>
		[DontCheckToken]
		public static Protocol<string> GetIOSVersionXml = new Protocol<string>();

		/// <summary>
		/// 获取所有global config；
		/// </summary>
		[DontCheckToken]
		public static Protocol<System.Collections.Generic.Dictionary<string, byte[]>> GetAllGlobalConfigSettings;

        /// <summary>
        /// 获取hotdll；
        /// </summary>
        [DontCheckToken]
        public static Protocol<System.Collections.Generic.Dictionary<string, byte[]>> GetIOSHotDll;

        /// <summary>
        /// 获取hotdll；
        /// </summary>
        [DontCheckToken]
        public static Protocol<System.Collections.Generic.Dictionary<string, byte[]>> GetAndroidHotDll;
    }
}

using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Threads;

namespace JustERP.Application.User.Wechat
{
    public class WechatConfig
    {
        public const string AppId = "wxd1e9929bab5029ce";
        public const string AppSecret = "644f585ce47f569406447cef3ebb04cf";
        public const string MerchantId = "1489631162";
        public const string PaySecret = "LianHeZixun586742POITFCijneik845";
        public const string TenpayNotify = "https://api.advisors-ally.com/api/Wechat/PayNotify/";

        /// <summary>
        /// 微信初始化
        /// </summary>
        public static void Init()
        {
            InitWeixinCache();
            InitWeixinLog();
            InitWeixin();
        }

        private static void InitWeixin()
        {
            AccessTokenContainer.Register(AppId, AppSecret);
        }

        static void InitWeixinCache()
        {
            ThreadUtility.Register();
        }

        static void InitWeixinLog()
        {
            //自定义日志记录回调
            Senparc.Weixin.WeixinTrace.OnLogFunc = () =>
            {
                //加入每次触发Log后需要执行的代码
            };

            //当发生基于WeixinException的异常时触发
            Senparc.Weixin.WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                
            };
        }
    }
}
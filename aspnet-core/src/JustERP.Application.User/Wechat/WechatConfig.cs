using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Threads;

namespace JustERP.Application.User.Wechat
{
    public class WechatConfig
    {
        public const string AppId = "wx3e099bc2f6f8d8f6";
        public const string AppSecret = "6198bb24b64343fcc745cbb70db6f318";

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
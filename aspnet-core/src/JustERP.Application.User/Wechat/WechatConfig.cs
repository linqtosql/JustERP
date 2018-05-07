using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Threads;

namespace JustERP.Application.User.Wechat
{
    public class WechatConfig
    {
        public const string AppId = "wx3e099bc2f6f8d8f6";
        public const string AppSecret = "6198bb24b64343fcc745cbb70db6f318";

        /// <summary>
        /// ΢�ų�ʼ��
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
            //�Զ�����־��¼�ص�
            Senparc.Weixin.WeixinTrace.OnLogFunc = () =>
            {
                //����ÿ�δ���Log����Ҫִ�еĴ���
            };

            //����������WeixinException���쳣ʱ����
            Senparc.Weixin.WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                
            };
        }
    }
}
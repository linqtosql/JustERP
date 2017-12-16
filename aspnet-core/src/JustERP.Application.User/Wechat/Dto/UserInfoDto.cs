using System.Collections.Generic;
using Abp.AutoMapper;
using JustERP.Core.User.Wechat;

namespace JustERP.Application.User.Wechat.Dto
{
    [AutoMapTo(typeof(LhzxExpertWechatInfo))]
    public class UserInfoDto
    {
        /// <summary>
        /// openid
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// nickname
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// sex
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// province
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// headimgurl
        /// </summary>
        public string Headimgurl { get; set; }
        /// <summary>
        /// privilege
        /// </summary>
        public List<string> Privilege { get; set; }
        /// <summary>
        /// unionid
        /// </summary>
        public string Unionid { get; set; }
    }
}

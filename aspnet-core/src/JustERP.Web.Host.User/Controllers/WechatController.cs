using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using JustERP.Web.Core.User.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JustERP.Web.Host.User.Controllers
{
    public class SignatureVerifyModel
    {
        public string signature { get; set; }
        public string timestamp { get; set; }
        public string nonce { get; set; }
        public string echostr { get; set; }
    }
    public class WechatController : JustERPControllerBase
    {
        public IActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            return new ContentResult { Content = echostr };
            var listStr = new string[3]
            {
                "lianhezixun",
                timestamp,
                nonce
            };
            listStr = listStr.OrderBy(s => s).ToArray();

            var str = string.Join(string.Empty, listStr);

            var sha1 = System.Security.Cryptography.SHA1.Create();

            var hash = sha1.ComputeHash(ObjectToByteArray(str));

            var hashStr = BitConverter.ToString(hash, 0).Replace("-", string.Empty).ToLower();

            if (hashStr == signature)
                return new ContentResult { Content = echostr };
            throw new Exception($"{signature},{echostr},{nonce},{timestamp}");
        }

        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}

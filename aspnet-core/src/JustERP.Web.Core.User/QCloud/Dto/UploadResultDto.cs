namespace JustERP.Web.Core.User.QCloud.Dto
{
    public class UploadResultDto
    {
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string access_url { get; set; }
            public string source_url { get; set; }
            public string url { get; set; }
            public string resource_path { get; set; }
        }
    }
}

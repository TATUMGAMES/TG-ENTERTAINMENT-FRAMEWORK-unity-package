namespace EntertainmentFramework.Utils.Rest
{
    public enum Verbs
    {
        PUT,
        GET,
        POST,
        PATCH,
        DELETE,
        CREATE,
        HEAD
    }

    public class ContentTypes
    {
        // more stuff found in http://www.freeformatter.com/mime-types-list.html
        public const string JSON = "application/json";
        public const string BINARY = "application/octet-stream";
        public const string XML = "application/xml";
        public const string MP4 = "audio/mp4";
        public const string OGG = "audio/ogg";
        public const string BITMAP = "image/bmp";
        public const string PNG = "image/png";
        public const string TIFF = "image/tiff";
        public const string CSV = "text/csv";
        public const string HTML = "text/html";
        public const string TEXT = "text/plain";
        public const string FORM = "application/x-www-form-urlencoded";
        public const string MULTIPART = "multipart/form-data";
        public const string MULTIPARTWITHBOUNDARY = "multipart/form-data; boundary=----WebKitFormBoundaryKTewU7J9DW1rNfmm";// + rndm.toString().substr(2);
    }
}
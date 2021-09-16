using static Mango.Web.ApplicationSettings;

namespace Mango.Web.Models.Base
{
    public class ApiRequest<DataType> where DataType : class
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public DataType Data { get; set; }
        public string AccessToken { set; get; }
    }
}

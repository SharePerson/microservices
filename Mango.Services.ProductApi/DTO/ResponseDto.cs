using System.Collections.Generic;

namespace Mango.Services.ProductApi.DTO
{
    public class ResponseDto<DataType> where DataType: class
    {
        public bool IsSuccess { get; set; } = true;
        public DataType Result { set; get; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; } = new();
    }
}

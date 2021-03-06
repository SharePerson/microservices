using System.Collections.Generic;

namespace Mango.Services.CouponApi.DTO
{
    public class ResponseDto<DataType>
    {
        public bool IsSuccess { get; set; } = true;
        public DataType Result { set; get; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; } = new();
    }
}

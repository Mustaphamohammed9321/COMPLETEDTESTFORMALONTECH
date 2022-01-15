using System;

namespace MALON_GLOBAL_WEBAPP.Models.DTO.ResponseDto
{
    public class UserLoginResponseDto
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime Date { get; set; }
        public string Token { get; set; }
    }
}

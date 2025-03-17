using System.ComponentModel;

namespace OpenWeather.Api.Dto
{
    public class ResponseApiDto<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        [Description("Constructor for ResponseDto")]
        public ResponseApiDto(T data, string message = "success")
        {
            Data = data;
            Message = message;
        }
        /// <summary>
        /// api data
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// api message
        /// </summary>
        public string Message { get; private set; }

    }

   
}

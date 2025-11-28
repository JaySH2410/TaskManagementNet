namespace TaskManagement.DTOs
{
    public class ApiResponse
    {
        public int statusCode { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string detail { get; set; }
        public object @object { get; set; }
        public DateTime timeStamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse Success(object obj, string message = "")
        {
            return new ApiResponse
            {
                statusCode = 200,
                isSuccess = true,
                message = message,
                @object = obj
            };
        }

        public static ApiResponse Error(string message, string detail = "", int statusCode = 500)
        {
            return new ApiResponse
            {
                statusCode = statusCode,
                isSuccess = false,
                message = message,
                detail = detail
            };
        }
    }
}

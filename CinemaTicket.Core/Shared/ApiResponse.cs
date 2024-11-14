namespace CinemaTicket.Core.Shared
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public T? Data { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public ApiResponse(T data)
        {
            Data = data;
            IsSuccess = true;
        }

        private ApiResponse() { }

        public static ApiResponse<T> Success(T data) => new ApiResponse<T> { IsSuccess = true, Data = data };
        public static ApiResponse<T> Success(T data, params string[] messages) => new ApiResponse<T> { IsSuccess = true, Data = data, Messages = messages.ToList() };
        public static ApiResponse<T> Failure(params string[] messages) => new ApiResponse<T> { IsSuccess = false, Messages = messages.ToList() };
    }
}

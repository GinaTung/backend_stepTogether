namespace stepTogether.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "操作成功")
        {
            return new ApiResponse<T> { Success = true, Message = message, Data = data };
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T> { Success = false, Message = message, Data = default };
        }
    }

    // ✅ 將這段挪到外面，並指定實體型別 string，避免泛型問題
    public class UnauthorizedExample : Swashbuckle.AspNetCore.Filters.IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return ApiResponse<string>.Fail("帳號或密碼錯誤");
        }
    }
}

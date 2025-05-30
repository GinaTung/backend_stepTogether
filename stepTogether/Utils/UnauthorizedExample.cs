using Swashbuckle.AspNetCore.Filters;
using stepTogether.Models;

public class UnauthorizedExample : IExamplesProvider<ApiResponse<string>>
{
    public ApiResponse<string> GetExamples()
    {
        return new ApiResponse<string>
        {
            Success = false,
            Message = "帳號或密碼錯誤",
            Data = null
        };
    }
}

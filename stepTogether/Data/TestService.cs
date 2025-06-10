using stepTogether.Data;
using stepTogether.Models;
using Supabase;

public class TestService
{
    private readonly Supabase.Client _client;

    public TestService(Supabase.Client client)
    {
        _client = client;
    }

    public async Task<List<Test>> GetAllTestsAsync()
    {
        var result = await _client
            .From<Test>() // ✅ 小寫表名
            .Get();

        return result.Models;
    }
}

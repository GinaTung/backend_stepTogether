using Supabase;

public class SupabaseService
{
    public Client SupabaseClient { get; private set; }

    public SupabaseService(IConfiguration configuration)
    {
        var url = configuration["Supabase:Url"];
        var key = configuration["Supabase:Key"];

        if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(key))
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            SupabaseClient = new Client(url, key, options);
            SupabaseClient.InitializeAsync().Wait();  // 記得初始化
        }
    }
}

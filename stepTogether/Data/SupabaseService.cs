using Supabase;
using Microsoft.Extensions.Configuration;

namespace stepTogether.Data
{
    public class SupabaseService
    {
        public Client SupabaseClient { get; }

        public SupabaseService(IConfiguration configuration)
        {
            var url = configuration["Supabase:Url"];
            var key = configuration["Supabase:Key"];

            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true,
                AutoRefreshToken = true
            };

            SupabaseClient = new Client(url, key, options);
            SupabaseClient.InitializeAsync().Wait();
        }
    }
}

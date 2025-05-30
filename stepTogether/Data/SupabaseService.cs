using Microsoft.EntityFrameworkCore;
using stepTogether.Controllers;
using stepTogether.Models;
using Supabase;
namespace stepTogether.Data
{
    public class SupabaseService
    {
        public Client SupabaseClient { get; }

        public SupabaseService()
        {
            var url = "https://aovtwkssjmquexxposfj.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFvdnR3a3Nzam1xdWV4eHBvc2ZqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc0NzgzNjM5MSwiZXhwIjoyMDYzNDEyMzkxfQ._wWhZ8m6FRuiuq3UMgm-fOhZSBmRkLTKJfEc_aj36YU"; // 從 Supabase → Project Settings → API → Service Role Key
            SupabaseClient = new Client(url, key);
            SupabaseClient.InitializeAsync().Wait();
        }
    }
}




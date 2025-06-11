using Newtonsoft.Json;
using Supabase.Postgrest;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


[Table("profiles")]
public class Profile : BaseModel
{
    [PrimaryKey("id", false)]
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }
}

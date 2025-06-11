using Newtonsoft.Json;
using Supabase.Postgrest;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


[Table("profiles")]
public class Profile : BaseModel
{
    [PrimaryKey("id", false)]
    [JsonProperty("id")]
    public string Id { get; set; }  // 自動產生，不從前端輸入

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }
}
public class CreateProfileDto
{
    public string Email { get; set; }
    public string Role { get; set; }
}

public class UpdateProfileDto
{
    public string Email { get; set; }
    public string Role { get; set; }
}
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
namespace stepTogether.Models
{
    [Table("test")]
    public class Test : BaseModel
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("testname")]
        public string TestName { get; set; }
        [Column("description")]
        public string Description { get; set; }
    }
}

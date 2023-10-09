using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTWebApp.Infrastructure.Database.Tables
{
    [Table("users")]
    public class USER
    {
        [Key]
        [Column("user_id")]
        public string Id { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("modified_by")]
        public string ModifiedBy { get; set; }

        [Column("web_session")]
        public string WebSession { get; set; }

        [Column("created")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [Column("modified")]
        [DataType(DataType.Date)]
        public DateTime Modified { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
